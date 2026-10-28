# EduMap-Hexagonal

Plataforma web para facilitar el acceso, consulta, ubicación y comparación de información sobre instituciones educativas en la región de Arequipa (colegios, academias, institutos y universidades).

## Descripción del Proyecto

El proyecto surge ante las dificultades que enfrentan los estudiantes y las familias al buscar y comparar alternativas educativas debido a la dispersión de información, la falta de transparencia en los costos y la necesidad de realizar consultas presenciales para conocer las características de cada institución.

Ante esta problemática, **EduMap** permite buscar instituciones mediante filtros, consultar sus características detalladas, visualizar sus ubicaciones en un mapa interactivo y comparar pensiones estimadas, oferta educativa y calificaciones. Asimismo, incorpora reseñas y valoraciones de la comunidad para facilitar la evaluación de las alternativas disponibles, reduciendo el tiempo dedicado a la búsqueda y contribuyendo a una toma de decisiones informada.

## Alcance del Sistema

El sistema comprende los siguientes módulos y funcionalidades:

* **Gestión de usuarios y perfiles:** Registro, inicio de sesión, administración de perfiles y control de acceso según roles (Usuarios, Representantes de instituciones educativas y Administradores).
* **Directorio de instituciones educativas:** Registro, consulta, actualización y eliminación de información institucional (nombre, categoría, descripción, dirección, distrito, oferta educativa, pensiones estimadas y datos de contacto).
* **Búsqueda y filtros dinámicos:** Búsqueda por nombre y filtros combinados por categoría, ubicación geográfica, rango de pensión y calificación promedio.
* **Mapa interactivo y geolocalización:** Visualización de instituciones educativas mediante marcadores en mapa interactivo con resumen de información y cálculo de distancias por coordenadas.
* **Fichas institucionales y comparador:** Vistas detalladas por institución y comparador simultáneo de múltiples alternativas para contrastar características clave.
* **Reseñas y calificaciones:** Publicación, edición y eliminación de opiniones ciudadanas con puntuación por estrellas, calculando automáticamente promedios y cantidad de reseñas.
* **Administración y reportes:** Gestión centralizada de usuarios, instituciones y reseñas, con reportes estadísticos sobre distribución por categorías, promedios y participación.

> **Nota de alcance:** El sistema no comprende la administración interna de matrículas, notas, asistencia u otros procesos académicos propios de las instituciones.

## Arquitectura del Proyecto

El backend está desarrollado bajo los principios de **Arquitectura Hexagonal (Puertos y Adaptadores)** combinada con el patrón **CQRS (Command Query Responsibility Segregation)** utilizando **MediatR**:

* **`Edumap.Domain`**: Núcleo del sistema. Contiene entidades del dominio e interfaces de repositorios (Puertos de Persistencia).
* **`Edumap.Application`**: Casos de uso divididos en Comandos y Consultas (`UseCases > [Entidad] > Commands / Queries`), DTOs y Puertos de servicios externos.
* **`Edumap.Infrastructure`**: Adaptadores secundarios para persistencia (Entity Framework Core con PostgreSQL), seguridad (BCrypt, JWT) y configuraciones técnicas.
* **`Edumap.Api`**: Adaptador primario REST. Controladores delgados que despachan solicitudes hacia la capa de aplicación a través de MediatR.

## Tecnologías Utilizadas

* **.NET 10** (C#)
* **Entity Framework Core 10**
* **PostgreSQL / Npgsql**
* **MediatR** (Patrón Mediador y CQRS)
* **JWT Bearer Authentication**
* **Swagger / OpenAPI**
