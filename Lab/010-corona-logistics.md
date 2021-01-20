# Corona Logistik

## Einleitung

Sie arbeiten in der IT-Abteilung der (fiktiven) HKG Einzelhandelskette. Ihr Unternehmen betreibt hunderte Filialen in ganz Österreich. Natürlich verfügt ihre Firma auch über eine ausgeklügelte Logistik, mit der die Filialen laufend mit Waren versorgt werden.

Im Zuge der Corona-Pandemie hat sich HKG entschieden, den Staat Österreich und insbesondere die österreichischen Schulen zu unterstützen, indem das Verteilen von Corona-Schnelltests an Schulen übernommen wird.

HKG hat außerdem zugesagt, ein Softwaresystem zur digitalen Koordination der notwendigen Prozesse zu erstellen. Das ist ihr Verantwortungsbereich.

## Funktionale Anforderungen

* Schulen können über eine Web-Anwendung ihren jeweiligen **Bedarf** an Testkits melden. Außerdem können die Schulen dabei die nächstgelegene HKG-Filiale auswählen, in der später die Pakete mit Testkits abgeholt werden können.

* Die Hersteller der Testkits liefern diese palettenweise an ein HKG Zentrallager, in dem die Testkits in kleinere Pakete für die jeweiligen Schulen umgepackt werden. Jedes **Pakete für eine Schule** wird mit einem **eindeutigen Code** (als Barcode und Zahl auf einem Aufkleber am Paket aufgedruckt) versehen.

* Die Pakete werden nach Meldung des Bedarf schnellstmöglich an die von der Schule angegebene Filiale geliefert.

* Nach Übernahme der Pakete in der Filiale tippen/scannen MitarbeiterInnen den Paketcode ein. Das IT-System **informiert** daraufhin automatisch **die Schule per Email** davon, dass die Testkits zur Abholung bereit sind.

* Eine Person der Schule kommt in die Filiale und erhält gegen Vorweis der zuvor genannten Email das Paket ausgehändigt. Das Paket wird im IT-System von FilialmitarbeiterInnen **als ausgehändigt markiert**.

* Beamte der Regierung müssen jederzeit eine **Statistik** mit der Anzahl der angeforderten und ausgelieferten Testkits je Schule abfragen können.

* Jeder Testkit ist mit einer **Chargennummer** versehen. Die Testkit-Hersteller liefern je Charge ein digital signiertes **Qualitätssicherungsdokument** in Form einer PDF-Datei. Diese Dateien sind bereits in einer existierenden SQL Server-Datenbank im Firmennetzwerk von HKG gespeichert. MitarbeiterInnen der Schule müssen jederzeit in der Lage sein, über die Web-Anwendung nach Eingabe einer Chargennummer das Qualitätssicherungsdokument herunterzuladen.

## Nicht-funktionale Anforderungen

* Das Softwaresystem muss so rasch wie möglich verfügbar sein.

* HKG darf mit dem Engagement werben, erhält für die Logistikleistungen aber keine finanzielle Vergütung. Daher ist darauf zu achten, dass die **Betriebskosten des Systems niedrig** sind.

* Es ist damit zu rechnen, dass es nach Ankündigung der Logistik-Partnerschaft zu einem **gleichzeitigen Ansturm** aller rund 6000 österreichischen Schulen kommt. Nach wenigen Tagen wird die Last weniger, da sich die Anforderungen zeitlich verteilen.

* Es wäre ein großer **Reputationsverlust**, wenn das IT-System dem erwarteten, initialen Ansturm nicht standhalten oder voller Fehler sein sollte, deren Behebung lange dauert. Das soll auf jeden Fall vermieden werden.

* Es ist damit zu rechnen, dass sich die **Anforderungen** in den nächsten Wochen immer wieder **ändern** und neue Funktionen dazu kommen. HKG möchte mit hoher Flexibilität und kurzen Umsetzungszeiten glänzen.

* Da das Softwaresystem nur einige Monate im Einsatz sein wird, ist **keine tiefergehende Integration** in sonstige IT-Systeme von HKG vorgesehen. Nur die Schnittstelle zur SQL Server-DB mit den Qualitätssicherungsdokumenten ist vorzusehen. Ansonsten ist das Testkit-Logistiksystem eigenständig.

* Es gibt keine Vorgaben, wie sich Benutzer (FilialmitarbeiterInnen, MitarbeiterInnen von Schulen, Beamte und Beamtinnen) **am System anmelden**. Sie können dafür beliebige Lösungen/Konzepte vorschlagen.

## Ihre Aufgabe

* Definieren Sie, mit welchen Programmiersprachen/Platformen Sie entwickeln werden.

* Erstellen Sie ein Architekturdiagramm für eine Umsetzung des IT-Systems in Microsoft Azure. Geben Sie dabei insbesondere an, wie sie Subscriptions und Resource Groups gliedern und welche Azure-Dienste sie einsetzen.

* Schätzen Sie monatlichen Azure-Kosten für das System ab. Geben sie eine Bandbreite der Kosten (Mindestkosten und Maximalkosten) an, mit denen zu rechnen ist.
