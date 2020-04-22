<!DOCTYPE html>
<html>

<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Welcome file</title>
  <link rel="stylesheet" href="https://stackedit.io/style.css" />
</head>

<body class="stackedit">
  <div class="stackedit__html"><h1 id="classroom-catastrophe-api">Classroom Catastrophe API</h1>
<h2 id="introduction">Introduction</h2>
<p>Hello EndlessStudios members. This GitHub repository contains our Unity project “ClassroomCatastrophe”, along with some useful documentation about how to design and develop tasks.</p>
<h2 id="creating-a-task">Creating a task</h2>
<h3 id="locating-the-files">Locating the files</h3>
<p>Inside the project directory, locate the folder named “CHALLENGES”</p>
<blockquote>
<p>PATH: <code>./Assets/Scripts/CHALLENGES</code></p>
</blockquote>
<p>The CHALLENGES folder contains 2 files as standing. <code>Challenge</code> and <code>TestChallenge</code>.</p>
<h3 id="knowing-your-files">Knowing your files</h3>
<p><code>Challenge</code> is an abstract class designed to work as a universal template for challenges, or “tasks”, this file doesn’t need to be edited in any way, and only serves as a connection between the <code>AbilityHitDetector</code> and the challenge file.</p>
<hr>
<p><code>AbilityHitDetector</code> is the main housing for challenges, abilities and score. This script manages the timing for your challenges, detects hits from teacher abilities, and alters score based on set variables within the challenge files.</p>
<hr>
<p><code>TestChallenge</code> is what you’ll be referencing when designing your challenge. If you have any worries, or are unsure about a certain method, reference this script prior to asking questions. The script, also known as Task 1, is the first task shown in-game, in which you steal Gaz’s hat. This script contains high-level methods which could come in handy down the line.</p>
<h3 id="creating-your-first-challenge">Creating your first challenge</h3>
<p>Inside the “CHALLENGES” folder, create a new script with <strong>YOUR TASK NUMBER</strong> as the title, following this syntax: <code>Level[LEVEL]Task[TASK].cs</code>. For example, <code>Level1Task2</code>.<br>
Double-click your new script, this should open either <strong>Visual Studio</strong> or <strong>Visual Studio Code</strong>.</p>
<blockquote>
<p><strong>PLEASE NOTE:</strong><br>
A dialogue window may pop up noting that “<em>The C# project “Assembly-CSharp” is targeting “.NETFramework,Version=v4.7.1”…</em>”, Don’t select anything, and press “ok”.<sup class="footnote-ref"><a href="#fn1" id="fnref1">1</a></sup></p>
</blockquote>
<p>Once you’ve loaded the script file, it should look similar to this:<br>
<img src="https://i.imgur.com/cpjKsKi.png" alt="Default Script"></p>
<p>Remove the body of the script, being everything inside the far-left brackets, indicated red.<br>
At the top of the class is the following line: <code>public class Level1Task2 : MonoBehaviour</code>; Change the word “MonoBehaviour” to “Challenge”.</p>
<p><em><strong>Oh no! we have our first error.</strong></em><br>
Can’t see it? The class name has become underlined in red. Hover your mouse over the red text and press <code>Show potential fixes</code>, then <code>Implement Abstract Class</code>. As you can see, the error has been removed, and 4 <code>protected functions</code> have been introduced,</p>
<pre class=" language-c"><code class="prism # language-c">   public override <span class="token keyword">void</span> <span class="token function">Hit</span><span class="token punctuation">(</span>AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates state<span class="token punctuation">,</span> bool hiding<span class="token punctuation">)</span>
    <span class="token punctuation">{</span>
        throw new System<span class="token punctuation">.</span><span class="token function">NotImplementedException</span><span class="token punctuation">(</span><span class="token punctuation">)</span><span class="token punctuation">;</span>
    <span class="token punctuation">}</span>

    public override <span class="token keyword">void</span> <span class="token function">Initialize</span><span class="token punctuation">(</span><span class="token punctuation">)</span>
    <span class="token punctuation">{</span>
        throw new System<span class="token punctuation">.</span><span class="token function">NotImplementedException</span><span class="token punctuation">(</span><span class="token punctuation">)</span><span class="token punctuation">;</span>
    <span class="token punctuation">}</span>

    public override <span class="token keyword">void</span> <span class="token function">Prompt</span><span class="token punctuation">(</span><span class="token punctuation">)</span>
    <span class="token punctuation">{</span>
        throw new System<span class="token punctuation">.</span><span class="token function">NotImplementedException</span><span class="token punctuation">(</span><span class="token punctuation">)</span><span class="token punctuation">;</span>
    <span class="token punctuation">}</span>

    protected override <span class="token keyword">void</span> <span class="token function">Main</span><span class="token punctuation">(</span><span class="token punctuation">)</span>
    <span class="token punctuation">{</span>
        throw new System<span class="token punctuation">.</span><span class="token function">NotImplementedException</span><span class="token punctuation">(</span><span class="token punctuation">)</span><span class="token punctuation">;</span>
    <span class="token punctuation">}</span>
</code></pre>
<h3 id="setting-the-defaults">Setting the defaults</h3>
<p>Each challenge has a set of default variables, which MUST be set before the challenge can be excecuted. Inside the <code>Main</code> function, initialize the following variables: <code>p_Completed, p_Failure and p_Hidden</code>.</p>

<table>
<thead>
<tr>
<th align="center">Variable</th>
<th align="center">Definition</th>
</tr>
</thead>
<tbody>
<tr>
<td align="center"><code>p_Completed</code></td>
<td align="center">The completed variable is the amount of points, or “likes” the player recieves upon completing the task.</td>
</tr>
<tr>
<td align="center"><code>p_Failure</code></td>
<td align="center">The failure variable is the amount of points, or “likes” the player recieves upon failing the task.</td>
</tr>
<tr>
<td align="center"><code>p_Hidden</code></td>
<td align="center">The hidden variable is the amount of points, or “likes” the player recieves upon hiding during a challenge, either under the desk or in their chair</td>
</tr>
</tbody>
</table><p>Here’s an example of the <code>Main</code> function containing the default definitions:</p>
<pre class=" language-c"><code class="prism # language-c">    protected override <span class="token keyword">void</span> <span class="token function">Main</span><span class="token punctuation">(</span><span class="token punctuation">)</span>
    <span class="token punctuation">{</span>
        p_Completed <span class="token operator">=</span> <span class="token number">60</span><span class="token punctuation">;</span> <span class="token comment">// Challenge Complete = +60 likes</span>
        p_Failure <span class="token operator">=</span> <span class="token operator">-</span><span class="token number">40</span><span class="token punctuation">;</span> <span class="token comment">// Challenge Failed = -40 likes</span>
        p_Hidden <span class="token operator">=</span> <span class="token number">20</span><span class="token punctuation">;</span> <span class="token comment">// Hidden during challenge = +20 likes</span>
    <span class="token punctuation">}</span>
</code></pre>
<p>With the defaults set, the challenge can now assign “likes”, or points, to the player upon completion, failure and hiding.</p>
<h3 id="the-hit">The Hit</h3>
<p>So once the player gets hit with a teacher ability during the course of a challenge being active, this function will call. But hey, you’re in luck! This function requires a certain code block, so just copy-paste the code below and replace the <code>Hit</code> function.</p>
<pre class=" language-c"><code class="prism # language-c">    public override <span class="token keyword">void</span> <span class="token function">Hit</span><span class="token punctuation">(</span>AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates state<span class="token punctuation">,</span> bool hiding<span class="token punctuation">)</span>
    <span class="token punctuation">{</span>
        <span class="token keyword">if</span> <span class="token punctuation">(</span>state <span class="token operator">!=</span> AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates<span class="token punctuation">.</span>NONE<span class="token punctuation">)</span>
        <span class="token punctuation">{</span>
            <span class="token keyword">int</span> amnt <span class="token operator">=</span> <span class="token number">0</span><span class="token punctuation">;</span>

            <span class="token keyword">if</span> <span class="token punctuation">(</span>state <span class="token operator">==</span> AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates<span class="token punctuation">.</span>HIDDEN <span class="token operator">||</span> hiding<span class="token punctuation">)</span> amnt <span class="token operator">=</span> p_Hidden<span class="token punctuation">;</span>
            <span class="token keyword">else</span> <span class="token keyword">if</span> <span class="token punctuation">(</span>state <span class="token operator">==</span> AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates<span class="token punctuation">.</span>NONE<span class="token punctuation">)</span> amnt <span class="token operator">=</span> <span class="token number">0</span><span class="token punctuation">;</span>
            <span class="token keyword">else</span> <span class="token keyword">if</span> <span class="token punctuation">(</span>state <span class="token operator">==</span> AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates<span class="token punctuation">.</span>ONGOING<span class="token punctuation">)</span> amnt <span class="token operator">=</span> p_Failure<span class="token punctuation">;</span>

            CloutHandler<span class="token punctuation">.</span><span class="token function">alterClout</span><span class="token punctuation">(</span>amnt<span class="token punctuation">)</span><span class="token punctuation">;</span>
        <span class="token punctuation">}</span>
    <span class="token punctuation">}</span>
</code></pre>
<h3 id="the-prompt-and-initialize">The Prompt and Initialize</h3>
<p>Ok so. These functions are very fiddily and need tweaking often to get them working correctly. Due to this, refrain from editing the <code>Initialize</code> and <code>Prompt</code> functions and leave that to Ben during progress updates on a <strong>WEDNESDAY</strong> and <strong>SUNDAY</strong>.</p>
<h3 id="so-how-does-the-challenge-work">So how does the challenge work?</h3>
<p>The challenge works the same as any other script. In Unity, a script does regular checks inside of a function called <code>Update</code>. Your challenge class DOES NOT reference MonoBehaviour, meaning the <code>Start</code>, <code>Update</code> and <code>FixedUpdate</code> functions do not work. <sup class="footnote-ref"><a href="#fn2" id="fnref2">2</a></sup></p>
<p>To work around this, the <code>AbilityHitDetector</code> runs a new function which works alongside the <code>Update</code> function. Inside your challenge, add the following code block:</p>
<pre class=" language-c"><code class="prism # language-c">public override <span class="token keyword">void</span> <span class="token function">Check</span><span class="token punctuation">(</span>AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates state<span class="token punctuation">,</span> GameObject player<span class="token punctuation">)</span>
<span class="token punctuation">{</span>
	<span class="token keyword">if</span><span class="token punctuation">(</span>state <span class="token operator">!=</span> AbilityHitDetector<span class="token punctuation">.</span>ChallengeStates<span class="token punctuation">.</span>NONE<span class="token punctuation">)</span>
    <span class="token punctuation">{</span>
		<span class="token comment">// Do something..</span>
	<span class="token punctuation">}</span>
<span class="token punctuation">}</span>
</code></pre>
<p>Replace <code>// Do Something</code> with your challenge code. Think of the Check function as the <code>Update</code> function in a normal Unity script, and treat it as such.<sup class="footnote-ref"><a href="#fn3" id="fnref3:1">3</a></sup></p>
<h3 id="what-now">What now?</h3>
<p>Good luck! Everything from here on can be googled easily, or you can reference the following documents;</p>
<p>C# — <a href="https://docs.microsoft.com/en-us/dotnet/csharp/">https://docs.microsoft.com/en-us/dotnet/csharp/</a><br>
Unity 2019.2.x — <a href="https://docs.unity3d.com/2019.2/Documentation/Manual/index.html">https://docs.unity3d.com/2019.2/Documentation/Manual/index.html</a></p>
<hr class="footnotes-sep">
<section class="footnotes">
<ol class="footnotes-list">
<li id="fn1" class="footnote-item"><p>Other windows may pop up, just continue to click “ok” <a href="#fnref1" class="footnote-backref">↩︎</a></p>
</li>
<li id="fn2" class="footnote-item"><p>Other, higher level, MonoBehaviour functions will not work; these include, but are not limited to, <code>Awake</code>, <code>OnTriggerEnter</code>, <code>OnCollisionEnter</code>, etc. see <sup class="footnote-ref"><a href="#fn3" id="fnref3">3</a></sup> for information. <a href="#fnref2" class="footnote-backref">↩︎</a></p>
</li>
<li id="fn3" class="footnote-item"><p>AbilityHitDetector detects other events too. To lessen the amount of errors and questions, the <code>OnTriggerEnter</code> and <code>OnCollisionEnter</code> functions have been removed. For access, message ben on discord and he’ll send over an updated AbilityHitDetector with your requested event. <a href="#fnref3" class="footnote-backref">↩︎</a> <a href="#fnref3:1" class="footnote-backref">↩︎</a></p>
</li>
</ol>
</section>
</div>
</body>

</html>
