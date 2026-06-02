# EcoSystem Connect - Proyecto Final

[cite_start]Este repositorio contiene la estructura base del proyecto EcoSystem Connect, construido utilizando una **Arquitectura N-Capas**  para garantizar la mantenibilidad y escalabilidad del sistema[cite: 551, 552].

## Estructura de Capas
* [cite_start]**EcoSystem.API (Capa de Presentación):** Proyecto ASP.NET Core Web API encargado exclusivamente de exponer los endpoints HTTP y comunicarse con el exterior[cite: 570, 571].
* [cite_start]**EcoSystem.Data (Capa de Acceso a Datos):** Biblioteca de clases responsable de la comunicación directa con la base de datos mediante Entity Framework Core[cite: 612, 613, 616].

[cite_start]**Regla de Oro Aplicada:** Cada capa tiene una responsabilidad única y la comunicación fluye de forma estrictamente descendente[cite: 553, 554].