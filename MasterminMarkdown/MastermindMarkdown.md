Aquí tens una explicació més humana i detallada del codi de *Mastermind*, amb exemples de com funciona cada part i com compleix els requisits.

---

### Robustesa
Aquest codi està dissenyat per evitar errors durant l'execució, ja que fa diverses comprovacions en les entrades de l'usuari. Això ajuda a que el joc no falli, fins i tot si l'usuari introdueix valors incorrectes.

**Exemple:** Si l'usuari introdueix una opció de dificultat no vàlida al menú principal, es mostrarà el missatge `"Opció no vàlida. Tria entre 1 i 6."`. Això s'aconsegueix amb un `switch` al mètode `SelectDifficulty`, que només accepta les opcions de l'1 al 6.

```csharp
switch (Console.ReadLine())
{
    case "1": return 10;
    case "2": return 6;
    case "3": return 4;
    case "4": return 3;
    case "5": return GetCustomAttempts();
    case "6": return 0;
    default:
        Console.WriteLine(InvalidOption);
        tries++;
        break;
}
```

### Claredat i ordre
El codi és clar i fàcil de llegir gràcies a l'ús de noms descriptius i constants definides al principi. També conté comentaris que ajuden a entendre què fa cada bloc de codi.

**Exemple:** El títol del joc i el menú principal es guarden com a constants (`Title` i `Menu`), fet que millora la claredat del codi. Això facilita la seva modificació sense afectar altres parts del programa.

```csharp
const string Title = "ASCII ART TITLE HERE";
const string Menu = " ________________________________ \n" +
                    "|1. Dificultat Novell: 10 intents  |\n" +
                    "|2. Dificultat Aficionat: 6 intents|\n" +
                    "|3. Dificultat Expert: 4 intents   |\n" +
                    "|4. Dificultat Màster: 3 intents   |\n" +
                    "|5. Dificultat Personalitzada      |\n" +
                    "|6. Salir                          |\n" +
                    " ---------------------------------- \n" +
                    "Selecciona la dificultat (1-6): ";
```

### Variables i constants
Les variables i constants tenen noms autodescriptius que faciliten la comprensió de la seva funció dins el programa. Per exemple, `maxAttempts` indica el nombre màxim d’intents permesos segons la dificultat triada. Els literals (textos) estan declarats com a constants, seguint les bones pràctiques.

**Exemple de nom de variable autodescriptiu:**
```csharp
int maxAttempts = 10; // Número màxim d'intents segons la dificultat
```

### Estructures de control
Les estructures de control (`if`, `for`, `switch`) estan ben implementades per guiar el flux del programa sense errors. A més, el codi evita usar `break` i `continue` innecessaris, mantenint un flux clar.

**Exemple:** El `for` al mètode `Game` controla cada intent de l'usuari, verificant si ha endevinat la combinació secreta i gestionant els intents restants.

```csharp
for (int attempt = 0; attempt < maxAttempts && !won; attempt++)
{
    Console.WriteLine(AttemptPrefix + $"{attempt}/{maxAttempts}");
    // Mostra la pista de l'intent anterior si no és el primer intent
    if (attempt > 0) { CompleteHint(hint, userCombination); }
}
```

### Gestió d'errors
El programa controla les possibles entrades incorrectes de l’usuari, especialment amb les entrades numèriques per a la combinació. Per exemple, només accepta números entre 1 i 6 i assegura que siguin exactament 4 dígits.

**Exemple:** Quan l'usuari introdueix una combinació, el codi valida que els valors siguin dins del rang esperat. Si no, mostra un missatge d'error.

```csharp
if (numbers.Length != 4)
{
    Console.WriteLine(InvalidFormat);
    tries++;
}
```

### Disseny modular
El programa utilitza mètodes per a cada funció del joc, fent el codi més organitzat i fàcil de mantenir. Això també permet reutilitzar codi quan sigui necessari.

**Exemple de mòdul:** El mètode `GenerateHint` crea una pista basada en la combinació secreta i l'intent de l'usuari, retornant un string amb 'O', 'Ø' i '×' per indicar si els números i posicions són correctes.

```csharp
public static string GenerateHint(int[] userCombination, int[] secretCombination)
{
    // Crea la pista per a la combinació
}
```

### Jocs de proves
En el video adjunt a aquest markdown es comprova:
1. Seleccionar cada dificultat al menú (1-6).
2. Entrar una combinació incorrecta (fora del rang de l'1-6).
3. Intentar guanyar i perdre amb diferents combinacions.
4. Provar una combinació amb números repetits.
5. Triar continuar o no al final del joc.
6. Verificar les pistes en diversos intents.
7. Triar una dificultat personalitzada i comprovar si els intents funcionen correctament.
8. Verificar si el joc torna al menú inicial després de cada partida.

### UX/UI
Els missatges estan dissenyats per guiar l'usuari en cada pas. La informació dels intents restants i les pistes milloren l’experiència de joc, i la pregunta de si vol continuar després de cada partida és una opció amigable.

### Colors
Cada número de l'intent es mostra amb un color diferent per fer-lo més visual. Els colors es configuren al mètode `Hint`, canviant el color de fons per cada número.

**Exemple:**

```csharp
switch (lastAttempt[i])
{
    case 1: Console.BackgroundColor = ConsoleColor.DarkBlue; break;
    case 2: Console.BackgroundColor = ConsoleColor.DarkGreen; break;
    case 3: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
    // i així per cada número
}
```

### Menú
Quan l’usuari acaba el joc, el menú es torna a mostrar gràcies al bucle `do-while` a `Main`, sense necessitat d'un `Exit`.

**Exemple de bucle:**

```csharp
do
{
    Console.Clear();
    ShowWelcomeMessage();
    maxAttempts = SelectDifficulty();
    maxAttempts = maxAttempts > 0 ? Game(maxAttempts) : maxAttempts;
} while (maxAttempts > 0);
```

### Estètica
El codi utilitza ASCII art per al títol i decoracions al menú, creant una estètica atractiva que dona un toc únic al joc i fa que l’experiència sigui més immersiva per a l’usuari.
