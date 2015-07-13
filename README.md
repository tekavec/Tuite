# Tuite
<h1>Tuite application</h1>
<p>
	Tuite is a simple console-based social networking application (similar to Twitter) satisfying the scenarios below. It is writen in .NET C# language.
</p>
<h3>
	Scenarios
</h3>
<p>
	<strong>Posting</strong>
</p>
<p>
	Alice can publish messages to a personal timeline:
	> Alice -> I love the weather today  
	> Bob -> Damn! We lost!  
	> Bob -> Good game though. 
</p>

<p>
	<strong>Reading</strong>
</p>
<p>
	Bob can view Alice’s timeline:
</p>
<p>
	> Alice 
	<br />
	I love the weather today (5 minutes ago) 
	<br />
	> Bob  
	<br />
	Good game though. (1 minute ago) 
	<br />
	Damn! We lost! (2 minutes ago) 
	<br />
</p>
<p>
	<strong>Following</strong>
</p>
<p>
	Charlie can subscribe to Alice’s and Bob’s timelines, and view an aggregated list of all subscriptions:
</p>
<p>
	> Charlie -> I’m in New York today! Anyone want to have a coffee?  
	<br />
	> Charlie follows Alice  
	<br />
	> Charlie wall  
	<br />
	Charlie - I’m in New York today! Anyone want to have a coffee? (2 seconds ago)  
	<br />
	Alice - I love the weather today (5 minutes ago) 
	<br />
	> Charlie follows Bob  
	<br />
	> Charlie wall  
	<br />
	Charlie - I’m in New York today! Anyone wants to have a coffee? (15 seconds ago)  
	<br />
	Bob - Good game though. (1 minute ago)  
	<br />
	Bob - Damn! We lost! (2 minutes ago)  
	<br />
	Alice - I love the weather today (5 minutes ago) 
</p>

<h2>
	Usage
</h2>
<p>
	Users submit commands to the application. There are four commands. "posting", "reading", etc. are not part of 
	the commands; commands always start with the user’s name.  
	<ul>
		<li>posting: [user name] -> [message]</li>
		<li>reading: [user name]</li>
		<li>following: [user name] follows [another user] </li>
		<li>wall: [user name] wall</li>
	</ul>
</p>

<h2>
	Installation
</h3>
<h2>
	Requirements
</h3>
<p>
	<ul>
		<li>.NET Framework 4.5</li>
		<li>Visual Studio 2013</li>
	</ul>

</p>
<p>
	Tuite uses three NuGet packages:
	<ul>
		<li>NUnit</li>
		<li>Moq</li>
		<li>Humanize</li>
	</ul>

</p>

<h3>
	Setup
</h3>
<p>
	Open Tuite.sln in Visual Studio 2013, set Tuite project as a startup project and hit the F5/Start command button.
</p>
