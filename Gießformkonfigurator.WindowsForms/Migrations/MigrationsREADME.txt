Eine Migration stellt eine Änderung an der Datenbankstruktur dar.

Der Ablauf sieht wie folgt aus:

1. Änderungen vornehmen (z.B. Grundplatte neues Attribut hinzufügen oder ein Attribut entfernen)
2. "Extras" --> "NuGet-Paket-Manager" --> "Paket-Manager-Console"
3. "add-migration 'Name' " --> es öffnet sich eine Migration Datei im Ordner "Migrations". Hier sind die Änderungen aufgelistet.
4. "update-database" --> Die Migrationen werden mit der Datenbank synchronisiert