# Battle Royale 1v1 🎮

Juego de batalla 1 vs 1 en una arena de plataformas 3D. Los jugadores se empujan mutuamente intentando reducir la vida del rival a 0 pisando plataformas de eliminación. Gana el último en pie o el que tenga más HP al acabarse el tiempo.

## Estado del proyecto

> 🚧 En desarrollo activo — prototipo jugable

### ✅ Implementado
- Grilla 9×9 con generación automática desde código
- 7 tipos de plataformas con colores diferenciados
- Movimiento del personaje: caminar, correr, saltar
- Rotación del personaje hacia la dirección de movimiento
- Empuje entre jugadores
- Sistema de HP (200 HP por jugador)
- Plataformas de eliminación — hacen entre 15 y 20 de daño al pisar y cada segundo
- Plataformas de habilidad (recarga común, especial y épica)
- Plataformas de lanzamiento con dirección configurable e input bloqueado durante el vuelo
- Plataformas de teletransporte con cooldown de 5 seg por par y cambio de color
- Paredes invisibles en los bordes de la arena
- Split screen vertical con cámara fija en tercera persona por jugador
- Dos jugadores en el mismo teclado con controles independientes
- 3 personajes con habilidades distintas: Arashiko, Psicogirl y Jeffrey
- Habilidad común: duplica la fuerza de empuje por 6 seg
- Habilidad especial: inmunidad a empuje y habilidades épicas por 3 seg
- Habilidades épicas únicas por personaje
- Temporizador de 10 minutos — gana el que tenga más HP al acabarse
- Condición de empate si ambos jugadores terminan con el mismo HP
- Barras de vida en pantalla para cada jugador
- Íconos de habilidades que se iluminan al estar cargadas
- Menú de selección de personaje con columna independiente por jugador
- Pantalla de resultado con botones de revancha y menú

### 🔜 Próximamente
- Modelos 3D de los personajes
- Feedback visual de habilidades activas (inmunidad, parálisis, velocidad)
- Sonidos y efectos de partículas
- Cuenta regresiva antes de empezar la partida
- Arranque automático desde el menú al ejecutar el juego

## Requisitos

- **Unity** 6000.x LTS o superior
- **Input System** package instalado

## Cómo abrir el proyecto

1. Cloná el repositorio
   ```bash
   git clone https://github.com/tuusuario/tu-repo.git
   ```
2. Abrí **Unity Hub**
3. Clic en **Add project from disk** y seleccioná la carpeta clonada
4. Abrí la escena `Assets/Scenes/MainMenu` para arrancar desde el menú

## Controles

| Acción | Jugador 1 | Jugador 2 |
|--------|-----------|-----------|
| Moverse | WASD | Flechas ↑↓←→ |
| Correr | Left Shift | Right Shift |
| Saltar | Space | Enter |
| Empujar | E | P |
| Habilidad común | 1 | U |
| Habilidad especial | 2 | I |
| Habilidad épica | 3 | O |

## Personajes

| Personaje | Habilidad épica |
|-----------|----------------|
| Arashiko | Paraliza enemigos en radio 3x3 casillas por 5 seg |
| Psicogirl | Se teletransporta 4 casillas hacia adelante |
| Jeffrey | Triplica la velocidad de movimiento por 10 seg |

## Tipos de plataforma

| Color | Tipo | Efecto |
|-------|------|--------|
| Gris claro | Normal | Sin efecto |
| Rojo | Eliminación | 15-20 de daño al pisar y cada segundo |
| Verde | Habilidad común | 2 seg = habilidad común disponible |
| Azul oscuro | Habilidad especial | 3 seg = habilidad especial disponible |
| Violeta | Habilidad épica | 5 seg = habilidad épica disponible |
| Amarillo | Lanzamiento | Empuja al jugador en una dirección |
| Celeste | Teletransporte | Teletransporta a otro portal (cooldown 5 seg por par) |

## Estructura del proyecto

```
Assets/
├── Prefabs/
│   ├── Platform_Normal.prefab
│   └── Player.prefab
├── Scripts/
│   ├── GridManager.cs
│   ├── Platform.cs
│   ├── PlayerMovement.cs
│   ├── PlayerPush.cs
│   ├── PlayerAbilities.cs
│   ├── PlayerHealth.cs
│   ├── TeleportManager.cs
│   ├── GameManager.cs
│   ├── UIManager.cs
│   ├── CharacterSelection.cs
│   └── GameData.cs
└── Scenes/
    ├── MainMenu.unity
    └── GameScene.unity
```
