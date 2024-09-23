**Architektur und Implementierung des Projekts**

Für dieses Projekt habe ich eine mehrschichtige Architektur gewählt, die es ermöglicht, die verschiedenen Teile der Anwendung klar zu trennen und eine saubere, modulare Struktur zu schaffen. Die gewählten Schichten sind:

Api: Diese Schicht dient als Schnittstelle zur Außenwelt und ermöglicht die Interaktion mit den API-Endpunkten.

Application: Hier befindet sich die Geschäftslogik der Anwendung, welche für die Verarbeitung der Anfragen verantwortlich ist.

Domain: In dieser Schicht sind die Kerndomänenmodelle und -regeln enthalten, die die wichtigsten Geschäftsprozesse definieren.

Infrastructure: Diese Schicht kümmert sich um die Implementierung der zugrunde liegenden Technologien, wie Datenbankzugriffe oder externe Dienste.


Zusätzlich habe ich eine Testsuite für die Anwendung implementiert, die sich hauptsächlich auf die Application-Schicht konzentriert. Hierbei wurde besonders das Testen der CommandHandler und CommandQuery-Funktionen berücksichtigt, um sicherzustellen, dass die Geschäftslogik korrekt implementiert und getestet wird.

**Verwendung von Design Patterns**

Um eine bessere Kontrolle über die Typen von Events und deren Verarbeitung zu gewährleisten, habe ich das Strategy Design Pattern implementiert. Diese Herangehensweise bietet die Flexibilität, verschiedene Event-Typen individuell zu handhaben und die Erweiterbarkeit der Anwendung sicherzustellen.

Zusätzlich habe ich die Trennung von Commands und Queries eingeführt, was der Prinzipien der CQRS (Command Query Responsibility Segregation) entspricht. Hierbei habe ich das Mediator-Pattern in Kombination mit der Bibliothek MediatR verwendet. Dies ermöglicht es, die Verantwortung zwischen verschiedenen Teilen der Anwendung klar zu trennen und die Wartbarkeit des Codes zu verbessern.

**Fehlerbehandlung**

Für die Behandlung von kritischen Fehlern in der Anwendung habe ich benutzerdefinierte Exceptions implementiert. Diese ermöglichen eine gezielte und aussagekräftige Fehlerbehandlung, sodass das Debugging und die Wartung des Systems erleichtert werden.

**API-Dokumentation mit Swagger**

Für den dritten Teil der Aufgabe habe ich Swagger integriert, um eine umfassende und benutzerfreundliche Darstellung der API-Endpunkte bereitzustellen. Swagger ermöglicht es, sowohl die Struktur der APIs als auch die Details der Ein- und Ausgaben transparent und klar darzustellen. Dies erleichtert es den Nutzern, die Schnittstellen korrekt zu verwenden und sorgt für eine reibungslose Kommunikation zwischen den verschiedenen Teilen der Anwendung.



