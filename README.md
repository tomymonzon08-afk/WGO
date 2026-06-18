# Battle Royale 1v1 🎮

Juego de batalla 1 vs 1 en una arena de plataformas 3D. Los jugadores se empujan mutuamente intentando que el rival caiga en las casillas de eliminación. Gana el último en pie.

## Estado del proyecto

> 🚧 En desarrollo activo — prototipo jugable

### ✅ Implementado
- Grilla 9×9 con generación automática desde código
- 7 tipos de plataformas con colores diferenciados
- Movimiento del personaje: caminar, correr, saltar
- Empuje entre jugadores
- Plataformas de eliminación (2 seg = eliminado)
- Plataformas de habilidad (recarga común, especial y épica)
- Plataformas de lanzamiento 
- Plataformas de teletransporte con cooldown de 5 seg 
- Paredes invisibles en los bordes de la arena
- Split screen vertical con cámara fija en tercera persona por jugador
- Dos jugadores en el mismo teclado con controles independientes
- Temporizador de 10 minutos con condición de empate
- Pantalla de resultado con botones de revancha y menú

### 🔜 Próximamente
- Personajes con tamaños y habilidades distintas
- Sistema de habilidades usables (común, especial, épica)
- Menú principal con selección de personaje
- Efectos visuales y sonidos
- UI de habilidades en pantalla

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
4. Abrí el proyecto y cargá la escena `Assets/Scenes/GameScene`

## Controles

| Acción | Jugador 1 | Jugador 2 |
|--------|-----------|-----------|
| Moverse | WASD | Flechas ↑↓←→ |
| Correr | Left Shift | Right Shift |
| Saltar | Space | Enter |
| Empujar | E | Numpad 0 / P |

## Tipos de plataforma

| Color | Tipo | Efecto |
|-------|------|--------|
| Gris claro | Normal | Sin efecto |
| Rojo | Eliminación | 2 seg encima = eliminado |
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
│   ├── TeleportManager.cs
│   ├── GameManager.cs
│   └── UIManager.cs
└── Scenes/
    └── GameScene.unity
```
