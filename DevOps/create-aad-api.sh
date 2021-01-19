#!/bin/bash

# Get app display name from command-line args
APP=$1
CLIENTID=$2

# Check if app registration exists
AADAPPREG=$(az ad app list --display-name $APP)
if [ $(echo $AADAPPREG | jq length) == '0' ]
then
    # App registration does not exist -> create it
    AADAPPREG=$(az ad app create --display-name $APP --available-to-other-tenants false \
        --identifier-uris https://azure-web-sample-api.net)
else
    # App registration exists -> we do not need to create a new one
    AADAPPREG=$(echo $AADAPPREG | jq '.[0]')
fi
# Get app id and object id from app registration JSON
APPID=$(jq '.appId' -r <<< $AADAPPREG)
OBJID=$(jq '.objectId' -r <<< $AADAPPREG)

# Pre-authorize client
until az rest --method GET --uri https://graph.microsoft.com/v1.0/applications/$OBJID > /dev/null 2>&1; do
    sleep 3s
done
PERMISSION=$(az rest --method GET --uri https://graph.microsoft.com/v1.0/applications/$OBJID \
     | jq '.api.oauth2PermissionScopes[] | select(.value == "user_impersonation") | .id' -r)
az rest --method PATCH --uri https://graph.microsoft.com/v1.0/applications/$OBJID \
    --headers 'Content-Type=application/json' --body "{api:{preAuthorizedApplications:[{appId:'$CLIENTID',delegatedPermissionIds:['$PERMISSION']}]}}"

# Return results as JSON
jq --arg key0 'appId' --arg value0 $APPID \
   --arg key1 'objectId' --arg value1 $OBJID \
    '. | .[$key0]=$value0 | .[$key1]=$value1' \
   <<<'{}'

# Cleanup command; just in case you need it for debugging purposes
# az ad app delete --id $(az ad app list --display-name $APP | jq .[0].appId -r)
