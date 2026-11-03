# 🦌 NaraPark — Recreación interactiva del Parque de Nara

**NaraPark** es un proyecto desarrollado en **Unity** que recrea de forma aproximada una parte del **Parque de Nara (Japón)**, conocido por sus ciervos que deambulan libremente entre los visitantes. El objetivo es construir un entorno 3D explorable, con elementos naturales, caminos y pequeñas interacciones que transmitan la esencia del lugar.

> Se trata de una recreación inspirada en el parque real, no de una reproducción a escala ni de un modelo geográfico exacto.

## 💡 ¿Por qué elegimos el Parque de Nara?

La elección del escenario tiene dos motivos personales:

- **Ada:** mi sobrina se llama **Nara**, precisamente por este parque japonés, así que recrearlo tenía un significado especial para mí.
- **Manuel:** conocía Nara por **_Inazuma Eleven_**, donde aparece la zona y el **Parque Deerfield** (*Deer Park*), lo que le dio una razón adicional para proponer este lugar.

Estas dos conexiones nos animaron a convertir un sitio real y reconocible en una experiencia virtual interactiva.

## 🌳 ¿Qué incluye el proyecto?

- **Entorno natural 3D:** terreno, vegetación, árboles, caminos y zonas con agua, inspirados en el paisaje del parque.
- **Elementos decorativos y de ambientación:** recursos 3D para dar forma a los recorridos y al escenario.
- **Personaje controlable en tercera persona:** personaje humano con movimiento mediante teclado, rotación según la dirección de desplazamiento y cámara que sigue al jugador.
- **Animaciones del personaje:** estados de reposo, caminar, correr y saltar, además de una animación de extensión de la mano para recoger y entregar objetos.
- **Ciervos animados:** modelos con estados de movimiento y reposo, controlados mediante animaciones y scripts en C#.
- **Interacción con los ciervos:** el jugador puede ofrecerles una galleta cuando dispone de ella y está suficientemente cerca.
- **Sistema de recogida de galletas:** puesto interactivo que permite recoger galletas, mantener un contador y controlar el tiempo entre recogidas.
- **Efectos visuales:** partículas de corazones al alimentar a una cierva y pruebas de niebla o bruma para ambientar algunas zonas.
- **Sonido ambiental:** reproducción de sonidos de ambiente para aportar mayor realismo e inmersión al recorrer el parque.
- **Efectos de sonido de interacción:** sonido de alimentación cuando una cierva recibe y come una galleta.
- **Puesto de galletas:** elemento del recorrido asociado a la mecánica de alimentación de los ciervos.

## 🧍 ¿Cómo funciona el personaje del jugador?

Se ha incorporado un **personaje 3D animado mediante Mixamo**, configurado para permitir la exploración del escenario en tercera persona.

Para su implementación se ha trabajado con:

1. **Modelo 3D:** importación del personaje desde Mixamo con su esqueleto y materiales correspondientes.
2. **Configuración del rig:** utilización del sistema Humanoid de Unity para permitir la compatibilidad entre las diferentes animaciones importadas.
3. **Animator Controller:** creación de un controlador de animaciones denominado `PersonajeAnimator`, encargado de gestionar los diferentes estados del personaje.
4. **Movimiento:** implementación del script `PlayerMovimiento` mediante `CharacterController`, que gestiona el desplazamiento, la rotación, la gravedad y el salto.
5. **Cámara en tercera persona:** implementación de una cámara que sigue al personaje y permite controlar la orientación mediante el ratón.
6. **Animaciones de locomoción:** integración de los estados `Idle`, `Walk`, `Run` y `Salto`, con transiciones controladas mediante parámetros del Animator.
7. **Animación de interacción:** incorporación del estado `Extender`, que reproduce el movimiento de extender la mano al recoger una galleta o alimentar a una cierva.

El movimiento del personaje se ha ajustado para que la animación de salto coincida lo mejor posible con el desplazamiento físico, la fuerza del salto y la gravedad.

## 🦌 ¿Cómo funcionan los ciervos?

Uno de los aspectos principales del proyecto es la integración de un **modelo 3D de ciervo obtenido de Sketchfab** y la adaptación de sus animaciones al comportamiento dentro del escenario.

Para ello se ha trabajado con:

1. **Modelo y animaciones:** importación del ciervo y configuración de su `Animator` para controlar estados como `Idle` y `Run`.
2. **Movimiento:** uso de un script en C# (`DeerMovement`) para gestionar el desplazamiento y coordinarlo con las animaciones.
3. **Navegación:** utilización de `NavMeshAgent` para permitir que los ciervos se desplacen por el terreno y seleccionen destinos de forma aleatoria.
4. **Interacción:** el script `AlimentarCierva` comprueba la proximidad del jugador y si dispone de galletas.
5. **Alimentación:** cuando se cumplen las condiciones, el jugador puede pulsar **`F`** para darle una galleta.
6. **Animación del jugador:** al entregar la galleta, el personaje reproduce la animación `Extender`, simulando el gesto de ofrecer comida.
7. **Respuesta visual:** aparece un efecto de partículas con corazones que representa la reacción del animal.
8. **Respuesta sonora:** se reproduce un sonido de alimentación cuando la cierva recibe la galleta.
9. **Reanudación del movimiento:** durante la interacción, la cierva se detiene temporalmente y, una vez finalizada, recupera su comportamiento habitual.

La integración requirió ajustar las transiciones de animación y el movimiento del modelo para evitar, por ejemplo, que se desplazase mientras reproducía la animación de reposo.

## 🍪 ¿Cómo funciona el puesto de galletas?

El escenario incorpora un puesto desde el que el jugador puede conseguir galletas para alimentar a los ciervos.

Esta mecánica se desarrolla mediante el script `PuestoGalletas`:

1. **Detección de proximidad:** se comprueba la distancia entre el jugador y el puesto.
2. **Recogida:** cuando el jugador está suficientemente cerca, puede pulsar **`E`** para recoger una galleta.
3. **Contador:** se incrementa el número de galletas disponibles.
4. **Animación:** el personaje reproduce `Extender` para simular que recoge una galleta.
5. **Tiempo de espera:** se incorpora un intervalo de dos segundos entre recogidas para evitar que el contador aumente repetidamente mientras se ejecuta la animación.

Las galletas recogidas se comparten mediante un contador estático (`PuestoGalletas.cookies`), que permite consultar la cantidad disponible desde los diferentes scripts de alimentación.

Cuando el jugador alimenta a una cierva, se descuenta una galleta del contador.

## 🛠️ Herramientas y recursos utilizados

| Herramienta o recurso | Uso en el proyecto |
| --- | --- |
| **Unity 2022.3.62f3** | Motor principal para construir y ejecutar el escenario 3D. |
| **C#** | Programación del comportamiento de los animales, movimiento del jugador, interacciones y efectos. |
| **Unity Terrain** | Creación y edición del terreno y su entorno natural. |
| **Unity Animator** | Configuración y transición entre animaciones del jugador y los ciervos. |
| **Character Controller** | Gestión del movimiento y las colisiones del personaje. |
| **NavMeshAgent** | Navegación y desplazamiento de los ciervos por el escenario. |
| **Unity Audio System** | Reproducción de sonido ambiental y efectos sonoros de alimentación. |
| **Unity Particle System** | Efectos de partículas, como los corazones que aparecen al alimentar a las ciervas. |
| **Unity Asset Store** | Obtención de modelos, materiales y otros recursos del escenario. |
| **Mixamo** | Recurso para modelos y animaciones del personaje. |
| **Sketchfab** | Obtención del modelo 3D del ciervo y sus animaciones. |
| **EasyRoads3D** | Recurso incluido en el repositorio para la construcción de caminos. |
| **Unity AI Navigation** | Paquete disponible en el proyecto para trabajar con navegación de agentes. |
| **Unity Input System** | Sistema de entrada incluido entre las dependencias del proyecto. |
| **Git y GitHub** | Control de versiones y colaboración. |
| **Git LFS** | Gestión de recursos de gran tamaño cuando sea necesario. |

Los modelos, texturas, sonidos y animaciones de terceros pertenecen a sus respectivos autores y están sujetos a sus licencias de uso.

## 📁 Estructura del repositorio

```text
NaraPark/
├── Assets/              # Escenas, scripts, modelos, materiales y otros recursos
├── EasyRoads3Dv3/       # Recursos del sistema de caminos
├── Packages/            # Dependencias del proyecto Unity
├── ProjectSettings/     # Configuración del proyecto
├── .gitattributes       # Configuración de atributos de Git
└── .gitignore           # Exclusiones de archivos temporales
```

## ▶️ Cómo abrir el proyecto

**Requisitos:** tener instalado **Unity Hub**, **Unity 2022.3.62f3** y **Git**. Se recomienda disponer de **Git LFS** si el repositorio contiene archivos gestionados con esta extensión.

1. Clona el repositorio:

   ```bash
   git lfs install
   git clone https://github.com/AdaXiang/NaraPark.git
   ```

2. Abre **Unity Hub** y selecciona **Add / Añadir proyecto desde disco**.
3. Selecciona la carpeta `NaraPark` y ábrela con **Unity 2022.3.62f3**.
4. Deja que Unity importe los recursos y resuelva los paquetes del proyecto.
5. Abre la escena principal desde el panel `Assets` y pulsa **Play**.

> **Nota:** algunos recursos proceden de plataformas externas. Si faltan modelos, materiales, sonidos o paquetes por motivos de licencia o distribución, puede ser necesario importarlos de nuevo desde su fuente original.

## 🚧 Dificultades encontradas

Durante el desarrollo se abordaron problemas relacionados con la importación de modelos 3D, la compatibilidad de materiales, las animaciones y el movimiento de los ciervos. También fue necesario tener en cuenta el tamaño de los archivos de terreno y texturas al utilizar GitHub.

En el desarrollo del personaje se trabajó en la configuración del rig Humanoid, la integración de diferentes clips de animación y el ajuste de las transiciones del Animator para conseguir que los estados de reposo, caminar, correr y saltar funcionasen correctamente.

También se ajustó la relación entre el movimiento físico del personaje y las animaciones, especialmente durante el salto, y se integraron nuevas interacciones con el puesto de galletas y los ciervos.

## 🔮 Posibles mejoras

- Incorporar un **NPC vendedor de galletas** con diálogo e interacción.
- Añadir **turistas NPC** que recorran el parque y den más vida al escenario.
- Mejorar la navegación y la evasión de obstáculos de los ciervos.
- Seguir optimizando el escenario y ampliando las interacciones disponibles.
- Incorporar más animaciones y comportamientos para los ciervos y el personaje.
- Ampliar el sistema de sonido ambiental con diferentes sonidos según las zonas del parque y las situaciones del juego.

## 👥 Autores

Proyecto realizado por **Ada y Manuel**.

## 🔗 Recursos de interés

- [Parque de Nara — información turística](https://www.visitnara.jp/destinations/area/nara-park/)
- [Unity](https://unity.com/)
- [Unity Asset Store](https://assetstore.unity.com/)
- [Mixamo](https://www.mixamo.com/)
- [Sketchfab](https://sketchfab.com/)
- [Inazuma Eleven — Nara y Deer Park](https://inazuma-eleven.fandom.com/wiki/Nara)
