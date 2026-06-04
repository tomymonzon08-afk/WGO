# WGO 🎮

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
- Plataformas de teletransporte con cooldown de 5 seg por par

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

## Controles (teclado)

| Acción | Tecla |
|--------|-------|
| Moverse | WASD |
| Correr | Left Shift |
| Saltar | Space |
| Empujar | E |

## Tipos de plataforma

| Color | Tipo | Efecto |
|-------|------|--------|
| Gris claro | Normal | Sin efecto |
| Rojo | Eliminación | 2 seg encima = eliminado |
| Verde | Habilidad común | 2 seg = habilidad común disponible |
| Azul | Habilidad especial | 3 seg = habilidad especial disponible |
| Violeta | Habilidad épica | 5 seg = habilidad épica disponible |
| Amarillo | Lanzamiento | Empuja al jugador en una dirección |
| Celeste | Teletransporte | Teletransporta a otro portal (cooldown 5 seg) |

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
│   └── TeleportManager.cs
└── Scenes/
    └── GameScene.unity
```
