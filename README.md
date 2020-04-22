# Classroom Catastrophe API
## Introduction
Hello EndlessStudios members. This GitHub repository contains our Unity project "ClassroomCatastrophe", along with some useful documentation about how to design and develop tasks.

## Creating a task
### Locating the files
Inside the project directory, locate the folder named "CHALLENGES"
> PATH: `./Assets/Scripts/CHALLENGES`

The CHALLENGES folder contains 2 files as standing. `Challenge` and `TestChallenge`. 

### Knowing your files
`Challenge` is an abstract class designed to work as a universal template for challenges, or "tasks", this file doesn't need to be edited in any way, and only serves as a connection between the `AbilityHitDetector` and the challenge file.

---

`AbilityHitDetector` is the main housing for challenges, abilities and score. This script manages the timing for your challenges, detects hits from teacher abilities, and alters score based on set variables within the challenge files.

---

`TestChallenge` is what you'll be referencing when designing your challenge. If you have any worries, or are unsure about a certain method, reference this script prior to asking questions. The script, also known as Task 1, is the first task shown in-game, in which you steal Gaz's hat. This script contains high-level methods which could come in handy down the line.

### Creating your first challenge
Inside the "CHALLENGES" folder, create a new script with **YOUR TASK NUMBER** as the title, following this syntax: `Level[LEVEL]Task[TASK].cs`. For example, `Level1Task2`.
Double-click your new script, this should open either **Visual Studio** or **Visual Studio Code**.

> **PLEASE NOTE:**
> A dialogue window may pop up noting that "*The C# project "Assembly-CSharp" is targeting ".NETFramework,Version=v4.7.1"...*", Don't select anything, and press "ok".[^1]

[^1]: Other windows may pop up, just continue to click "ok"

Once you've loaded the script file, it should look similar to this:
![Default Script](https://i.imgur.com/cpjKsKi.png)

Remove the body of the script, being everything inside the far-left brackets, indicated red.
At the top of the class is the following line: `public class Level1Task2 : MonoBehaviour`; Change the word "MonoBehaviour" to "Challenge".

***Oh no! we have our first error.***
Can't see it? The class name has become underlined in red. Hover your mouse over the red text and press `Show potential fixes`, then `Implement Abstract Class`. As you can see, the error has been removed, and 4 `protected functions` have been introduced, 

```c#
   public override void Hit(AbilityHitDetector.ChallengeStates state, bool hiding)
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Prompt()
    {
        throw new System.NotImplementedException();
    }

    protected override void Main()
    {
        throw new System.NotImplementedException();
    }
```

### Setting the defaults
Each challenge has a set of default variables, which MUST be set before the challenge can be excecuted. Inside the `Main` function, initialize the following variables: `p_Completed, p_Failure and p_Hidden`.

| Variable | Definition |
|:--------:|:----------:|
|`p_Completed`|The completed variable is the amount of points, or "likes" the player recieves upon completing the task.|
|`p_Failure`|The failure variable is the amount of points, or "likes" the player recieves upon failing the task.|
|`p_Hidden`|The hidden variable is the amount of points, or "likes" the player recieves upon hiding during a challenge, either under the desk or in their chair|

Here's an example of the `Main` function containing the default definitions:
```c#
    protected override void Main()
    {
        p_Completed = 60; // Challenge Complete = +60 likes
        p_Failure = -40; // Challenge Failed = -40 likes
        p_Hidden = 20; // Hidden during challenge = +20 likes
    }
```
With the defaults set, the challenge can now assign "likes", or points, to the player upon completion, failure and hiding.

### The Hit
So once the player gets hit with a teacher ability during the course of a challenge being active, this function will call. But hey, you're in luck! This function requires a certain code block, so just copy-paste the code below and replace the `Hit` function.
```c#
    public override void Hit(AbilityHitDetector.ChallengeStates state, bool hiding)
    {
        if (state != AbilityHitDetector.ChallengeStates.NONE)
        {
            int amnt = 0;

            if (state == AbilityHitDetector.ChallengeStates.HIDDEN || hiding) amnt = p_Hidden;
            else if (state == AbilityHitDetector.ChallengeStates.NONE) amnt = 0;
            else if (state == AbilityHitDetector.ChallengeStates.ONGOING) amnt = p_Failure;

            CloutHandler.alterClout(amnt);
        }
    }
```

### The Prompt and Initialize
Ok so. These functions are very fiddily and need tweaking often to get them working correctly. Due to this, refrain from editing the `Initialize` and `Prompt` functions and leave that to Ben during progress updates on a **WEDNESDAY** and **SUNDAY**.

### So how does the challenge work?
The challenge works the same as any other script. In Unity, a script does regular checks inside of a function called `Update`. Your challenge class DOES NOT reference MonoBehaviour, meaning the `Start`, `Update` and `FixedUpdate` functions do not work. [^2]

[^2]: Other, higher level, MonoBehaviour functions will not work; these include, but are not limited to, `Awake`, `OnTriggerEnter`, `OnCollisionEnter`, etc. see [^3] for information.

To work around this, the `AbilityHitDetector` runs a new function which works alongside the `Update` function. Inside your challenge, add the following code block:
```c#
public override void Check(AbilityHitDetector.ChallengeStates state, GameObject player)
{
	if(state != AbilityHitDetector.ChallengeStates.NONE)
    {
		// Do something..
	}
}
```
Replace `// Do Something` with your challenge code. Think of the Check function as the `Update` function in a normal Unity script, and treat it as such.[^3]

[^3]: AbilityHitDetector detects other events too. To lessen the amount of errors and questions, the `OnTriggerEnter` and `OnCollisionEnter` functions have been removed. For access, message ben on discord and he'll send over an updated AbilityHitDetector with your requested event.

### What now?
Good luck! Everything from here on can be googled easily, or you can reference the following documents;

C# --- [https://docs.microsoft.com/en-us/dotnet/csharp/](https://docs.microsoft.com/en-us/dotnet/csharp/)
Unity 2019.2.x --- [https://docs.unity3d.com/2019.2/Documentation/Manual/index.html](https://docs.unity3d.com/2019.2/Documentation/Manual/index.html)



