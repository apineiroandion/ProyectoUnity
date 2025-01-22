# Proyecto Unity
## Angel Piñeiro Andion
### Proyecto para PMDM 2 DAM

### Entrega player y camaras.

El script correpondiente al movimiento del jugador es PlayerController, que es el resultado final de haber completado la guia de unity de rollaball.


Y el movimiento de la cmara implementado en el juego es cameratotalcontroller.

# Camera Controller Script

Este script de Unity controla la cámara en un juego, permitiendo cambiar entre diferentes estilos de cámara y rotar la cámara según la entrada del ratón. A continuación se detalla el funcionamiento a nivel técnico y lógico.

## Variables

### Variables públicas:
- **`public GameObject player;`**: Referencia al objeto del jugador, el cual la cámara seguirá.
- **`public float rotationSpeed;`**: Velocidad de rotación de la cámara cuando está en el estilo `ThirdPerson`.
- **`public float mouseSensitivity;`**: Sensibilidad del ratón, que afecta la rotación de la cámara en el estilo `FirstPerson`.

### Variables privadas:
- **`private Vector3 offset;`**: Distancia entre la cámara y el jugador, utilizada para mantener la cámara a una posición fija en relación al jugador cuando no está en primera persona.
- **`private enum CameraStyle`**: Enum que define tres estilos de cámara: `Static`, `FirstPerson` y `ThirdPerson`.
- **`private CameraStyle currentStyle = CameraStyle.ThirdPerson;`**: Estilo de cámara inicial. En este caso, se inicia con `ThirdPerson`.
- **`private float horizontalRotation = 0f;`**: Rotación horizontal de la cámara en el estilo `FirstPerson`.
- **`private float verticalRotation = 0f;`**: Rotación vertical de la cámara en el estilo `FirstPerson`.
- **`private float verticalRotationLimit = 80f;`**: Límite para la rotación vertical en el estilo `FirstPerson` (para evitar giros extremos).

## Métodos

### `Start()`
Este método se ejecuta al inicio:
- Calcula la distancia inicial entre la cámara y el jugador, almacenándola en la variable `offset`.
- Bloquea el cursor y lo hace invisible para mejorar la experiencia en los modos `FirstPerson` y `ThirdPerson`.

### `Update()`
Este método se llama cada frame:
- Permite cambiar entre los estilos de cámara presionando un botón definido en el Input Manager (por defecto "SwitchCamera").
- Si el estilo de cámara es `FirstPerson`, se captura la entrada del ratón y se calcula la rotación de la cámara:
  - **Rotación horizontal**: Se obtiene de la entrada en el eje X del ratón y se multiplica por la sensibilidad.
  - **Rotación vertical**: Se obtiene de la entrada en el eje Y del ratón y se multiplica por la sensibilidad. Luego se limita para evitar giros extremos (entre -80 y 80 grados).

### `LateUpdate()`
Este método se llama después de que se hayan procesado todos los objetos en la escena:
- Se ajusta la posición y la rotación de la cámara dependiendo del estilo seleccionado:
  - **`Static`**: La cámara se mantiene fija en relación al jugador, pero siempre lo sigue mirando.
  - **`FirstPerson`**: La cámara sigue al jugador y se mueve según el ratón, simulando la vista en primera persona.
  - **`ThirdPerson`**: La cámara rota alrededor del jugador a una velocidad constante, manteniendo al jugador centrado en la vista.

### `SwitchCameraStyle()`
Este método cambia cíclicamente entre los diferentes estilos de cámara:
- La variable `currentStyle` cambia de forma cíclica entre los valores del enum `CameraStyle` (de `Static` a `FirstPerson` a `ThirdPerson` y luego de vuelta a `Static`).

## Funcionamiento Técnico

### Entrada:
- El ratón controla la rotación de la cámara en el estilo `FirstPerson`.
- El usuario puede cambiar el estilo de la cámara usando una entrada personalizada (`SwitchCamera`), configurada en el Input Manager de Unity.

### Procesamiento:
- El script calcula las rotaciones horizontales y verticales basadas en la entrada del ratón.
- Se actualiza la posición y rotación de la cámara según el estilo seleccionado.

### Salida:
- La cámara se mueve y rota en la escena para seguir al jugador, ya sea en un estilo estático, en primera persona o en tercera persona.

## Comportamiento Lógico
- Al iniciar el juego, la cámara sigue al jugador desde una perspectiva en tercera persona.
- El jugador puede alternar entre cámara estática, primera persona y tercera persona presionando un botón de entrada.
- En el modo `FirstPerson`, la cámara rota de acuerdo a la entrada del ratón, proporcionando una vista inmersiva.
- En el modo `ThirdPerson`, la cámara rota alrededor del jugador, pero lo mantiene siempre en el centro de la vista.
- El cambio de estilo de cámara es cíclico y controlado por la entrada del jugador.

## IMPORTANTE!

### La implementacion de la camara en primera persona no la tengo implementada desde este script. El que gestiona la camara en primera persona es "camera2"

# Camera 2 Script

Este script de Unity controla una cámara en un juego, permitiendo que siga al jugador y responda a los movimientos del ratón para rotar la vista en los ejes X y Y.

## Variables

### Variables públicas:
- **`public GameObject player;`**: Referencia al objeto del jugador que la cámara seguirá.
- **`public float sensitivityX;`**: Sensibilidad del ratón en el eje X (movimiento horizontal).
- **`public float sensitivityY;`**: Sensibilidad del ratón en el eje Y (movimiento vertical).
- **`public float minimumY;`**: Límite inferior para la rotación vertical de la cámara.
- **`public float maximumY;`**: Límite superior para la rotación vertical de la cámara.

### Variables privadas:
- **`private Vector3 offset;`**: Distancia inicial entre la cámara y el jugador, utilizada para mantener la cámara a una posición fija en relación al jugador.
- **`private float rotationX = 0f;`**: Rotación acumulada en el eje X (horizontal) basada en el movimiento del ratón.
- **`private float rotationY = 0f;`**: Rotación acumulada en el eje Y (vertical) basada en el movimiento del ratón.

## Métodos

### `Start()`
Este método se ejecuta al inicio:
- Calcula la distancia inicial entre la cámara y el jugador, almacenándola en la variable `offset`. Esto asegura que la cámara mantenga una distancia fija del jugador durante el juego.

### `LateUpdate()`
Este método se llama después de que se hayan procesado todos los objetos en la escena:
- Captura el movimiento del ratón:
  - **Eje X**: Movimiento horizontal del ratón se usa para rotar la cámara alrededor del eje Y.
  - **Eje Y**: Movimiento vertical del ratón se usa para rotar la cámara alrededor del eje X.
- Limita la rotación vertical (eje Y) utilizando `Mathf.Clamp`, para evitar rotaciones extremas que puedan romper la experiencia de juego.
- Aplica la rotación acumulada al transform de la cámara utilizando `Quaternion.Euler`.
- Actualiza la posición de la cámara para que mantenga la distancia original desde el jugador (usando el valor `offset` calculado al principio).

## Funcionamiento Técnico

### Entrada:
- El movimiento del ratón controla la rotación de la cámara.
  - El eje X del ratón mueve la cámara horizontalmente.
  - El eje Y del ratón mueve la cámara verticalmente, con límites establecidos para evitar giros extremos.

### Procesamiento:
- La rotación de la cámara se acumula a lo largo del tiempo en los ejes X y Y.
- La rotación en el eje Y (vertical) se limita entre los valores de `minimumY` y `maximumY`.
- Se aplica la rotación calculada al objeto cámara utilizando `Quaternion.Euler`, lo que permite una rotación suave y fluida.

### Salida:
- La cámara se mueve para seguir al jugador, pero con una rotación ajustable según la entrada del ratón.
- La cámara mantiene su posición relativa al jugador utilizando el valor `offset`.

## Comportamiento Lógico
- Al iniciar el juego, la cámara sigue al jugador desde una posición inicial calculada en el `Start()`.
- La cámara rota de acuerdo con el movimiento del ratón en los ejes X y Y, permitiendo una vista libre y ajustable.
- La rotación en el eje Y está limitada para evitar giros indeseados (por ejemplo, ver el suelo o el cielo de manera excesiva).
- La posición de la cámara se actualiza para mantener una distancia constante del jugador, independientemente de la rotación.



