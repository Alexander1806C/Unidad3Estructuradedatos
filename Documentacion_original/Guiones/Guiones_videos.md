# Guiones de los tres videos de la Unidad 3

## Preparación común

Abre el proyecto y el PDF correspondiente. Captura tu pantalla y agrega tu cámara en la esquina inferior derecha, sin tapar código, botones o resultados. Comprueba el micrófono con una grabación breve. Todos los videos deben ser públicos y estar en el mismo canal del alumno, incluido el utilizado en la Unidad 2. Explica con tus palabras y muestra tu propia ejecución.

## Video 3.1: Pilas y colas estáticas (6 a 8 minutos)

### 0:00-0:40 | Presentación
“Soy [nombre]. Implementé desde cero una pila, una cola simple y una cola circular usando arreglos de capacidad fija. En estas clases no utilicé las colecciones nativas de C#.”
Muestra los tres archivos de la carpeta `Static`.

### 0:40-2:10 | Pila
Abre `StaticStack.cs`. Señala `items`, `top=-1` y las validaciones.
“Push incrementa top y almacena un elemento. Pop consulta la cima, limpia esa casilla y decrementa top. Peek consulta sin eliminar. Está vacía cuando top es menos uno y llena cuando top llega a capacidad menos uno.”
En la pestaña 3.1 crea una pila de capacidad tres. Inserta 10, 20 y 30; consulta Peek, intenta insertar 40 y después retira. Explica por qué primero sale 30 y el rechazo no cambia los datos.

### 2:10-3:50 | Cola simple
“La cola simple conserva el orden FIFO. Elegí una variante lineal sin desplazar elementos. front es el siguiente a salir y rear es el último insertado. Cuando se vacía, reinicio ambos índices.”
Crea capacidad tres; inserta 10, 20 y 30; retira 10 e intenta insertar 40.
“Queda una casilla libre al principio, pero no se reutiliza todavía. IsFull significa que no puedo insertar al final. Esta limitación permite comparar la cola simple con la circular.”
Vacía la cola y demuestra que se puede insertar otra vez.

### 3:50-5:30 | Cola circular
Abre `CircularQueue.cs` y señala `(rear+1)%Capacity`.
“En esta implementación rear indica la próxima inserción. Los índices regresan a cero mediante módulo. Uso Count para distinguir vacío y lleno porque en ambos casos front puede ser igual a rear.”
Repite la secuencia 10, 20, 30; retirar 10; insertar 40. Muestra el orden lógico 20, 30, 40 y retira los tres.

### 5:30-7:00 | Pruebas y conclusión
Ejecuta el comando `test`. Explica que las pruebas cubren capacidad uno, cola vacía, cola llena, valor cero, múltiples vueltas y comparación con modelos independientes.
“Push, Pop, Enqueue y Dequeue realizan trabajo constante en estas implementaciones; copiar o dibujar todos los elementos cuesta tiempo lineal. Estática se refiere a capacidad fija, no a la palabra clave static.”
Muestra el repositorio real.

## Video 3.2: Estructuras dinámicas y comparación (6 a 8 minutos)

### 0:00-1:00 | Nodos
“Una lista enlazada organiza elementos mediante nodos. Cada nodo propio contiene Value y Next. head apunta al primero y tail al último. Cuando está vacía ambos son null.”
Muestra `SinglyLinkedList.cs` y el diagrama del reporte.

### 1:00-2:45 | Operaciones propias
En la pestaña 3.2 agrega al final 10 y 20, agrega al inicio 5, elimina 20 y agrega 30. Explica la actualización de tail al borrar el último nodo.
“Insertar al inicio o al final cuesta O(1) porque conservo head y tail. Buscar una posición o eliminar por valor requiere recorrer nodos y cuesta O(n). No puedo acceder directamente al elemento número i como en un arreglo.”

### 2:45-4:00 | Pilas y colas dinámicas
Muestra `LinkedStack.cs` y `LinkedQueue.cs`.
“La pila agrega y retira en la cabeza. La cola agrega al final y retira en la cabeza. Ambas reutilizan mi lista propia y mantienen orden LIFO o FIFO.”
Ejecuta la demostración `dynamic` y señala sus salidas.

### 4:00-5:30 | Genéricos de .NET
Muestra `NativeExamples.cs` o pulsa el botón de ejemplos.
“También utilicé Stack, Queue, List y LinkedList genéricas, como permite esta tarea. List es un arreglo que puede cambiar de capacidad; LinkedList es doblemente enlazada. Que una colección crezca no significa que use nodos.”

### 5:30-7:00 | Comparación
Muestra las tablas de memoria y complejidad.
“Un arreglo fijo reserva casillas aunque no se usen. Una lista enlazada evita reservar casillas futuras, pero cada nodo necesita referencias y metadatos. Tampoco tiene memoria ilimitada. La mejor elección depende del tamaño esperado, las operaciones frecuentes y el costo de localizar elementos.”
Aclara que el ejemplo de bytes del PDF es hipotético, no una medición del runtime.

## Video 3.3: Historial con dos pilas (6 a 8 minutos)

### 0:00-0:45 | Problema elegido
“Elegí simular el historial de navegación. El programa administra la página actual y permite regresar o avanzar. Es una simulación: no abre sitios ni se conecta a internet.”

### 0:45-2:00 | Diagrama de clases
Muestra el diagrama del PDF.
“El formulario usa BrowserHistory. El historial contiene dos LinkedStack propias; cada pila utiliza una SinglyLinkedList con nodos. Una pila guarda las páginas anteriores y otra las disponibles para avanzar.”

### 2:00-4:00 | Demostración principal
Visita A, B y C. Pulsa Atrás dos veces, Adelante una vez y visita D. Explica cada transferencia mirando las pilas.
“Al retroceder, la página actual se apila en Adelante y se extrae la cima de Atrás. Al avanzar ocurre lo contrario. Al visitar D después de retroceder, se descarta la rama donde estaba C: ya no se debe poder avanzar a C.”
Muestra que el botón Adelante queda deshabilitado y guarda el registro de prueba.

### 4:00-5:00 | Entradas y casos límite
Intenta una entrada vacía, reinicia y comprueba que Atrás/Adelante estén deshabilitados. Visita dos veces la misma etiqueta y explica que son visitas distintas. El código rechaza entradas demasiado largas antes de alterar el estado.

### 5:00-6:15 | Pruebas automáticas
Ejecuta `test`. Explica que el historial se compara contra una lista cronológica con un cursor, para no duplicar exactamente la implementación de dos pilas. Lee el resultado real de tu ejecución.

### 6:15-7:15 | Justificación y límites
“La pila se adapta al acceso a la visita más reciente. Las operaciones estructurales del modelo usan una cantidad constante de enlaces, pero actualizar las listas de la interfaz recorre el historial. El registro puede crecer y la sesión se pierde al cerrar, salvo el texto exportado. No se usa una cola FIFO porque no necesitamos volver a la visita más antigua.”
Termina mostrando el repositorio real y el archivo de evidencia guardado.

## Lista de entrega

- Completar datos de portada.
- Ejecutar las tres pestañas en Windows y guardar capturas propias si se solicita la interfaz gráfica.
- Revisar el código, los casos límite y el significado de los índices.
- Publicar el repositorio GitHub y pegar su URL real.
- Grabar los tres videos con pantalla y cámara inferior derecha.
- Publicar como Públicos en el mismo canal del alumno.
- Verificar enlaces sin iniciar sesión y regenerar los PDF con los enlaces completos.
