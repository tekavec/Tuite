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
</p>
<p>
	<code>
		> Alice -> I love the weather today  
	</code>	
	<br />
	<code>
		> Bob -> Damn! We lost!  
	</code>
	<br />
	<code>
		> Bob -> Good game though. 
	</code>
</p>

<p>
	<strong>Reading</strong>
</p>
<p>
	Bob can view Alice’s timeline:
</p>
<p>
	<code>
		> Alice 
	</code>	
	<br />
	<code>
		I love the weather today (5 minutes ago) 
	</code>	
	<br />
	<code>
		> Bob  
	</code>	
	<br />
	<code>
		Good game though. (1 minute ago) 
	</code>	
	<br />
	<code>
		Damn! We lost! (2 minutes ago) 
	</code>	
</p>
<p>
	<strong>Following</strong>
</p>
<p>
	Charlie can subscribe to Alice’s and Bob’s timelines, and view an aggregated list of all subscriptions:
</p>
<p>
	<code>
		> Charlie -> I’m in New York today! Anyone want to have a coffee?  
	</code>	
	<br />
	<code>
		> Charlie follows Alice  
	</code>	
	<br />
	<code>
		> Charlie wall  
	</code>	
	<br />
	<code>
		Charlie - I’m in New York today! Anyone want to have a coffee? (2 seconds ago)  
	</code>	
	<br />
	<code>
		Alice - I love the weather today (5 minutes ago) 
	</code>	
	<br />
	<code>
		> Charlie follows Bob  
	</code>	
	<br />
	<code>
		> Charlie wall  
	</code>	
	<br />
	<code>
		Charlie - I’m in New York today! Anyone wants to have a coffee? (15 seconds ago)  
	</code>	
	<br />
	<code>
		Bob - Good game though. (1 minute ago)  
	</code>	
	<br />
	<code>
		Bob - Damn! We lost! (2 minutes ago)  
	</code>	
	<br />
	<code>
		Alice - I love the weather today (5 minutes ago) 
	</code>	
</p>

<h2>
	Usage
</h2>
<p>
	Users submit commands to the console application. All users use the same console. Users submits their messages and read messges from other users with the command syntax described below.
</p>
<p>	
	There are four commands. "posting", "reading", etc. are not part of 
	the commands; commands always start with the user’s name.  
	<ul>
		<li>posting: [user name] -> [message]</li>
		<li>reading: [user name]</li>
		<li>following: [user name] follows [another user] </li>
		<li>wall: [user name] wall</li>
	</ul>
</p>
</p>
	There is no special user registration command - non existing users are created when they post their firs message. 
<p>
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
		<li>NuGet 2.8</li>
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
	<ul>
		<li>Open the "Tuite.sln" file with the Visual Studio 2013</li>
		<li>Set the "Tuite" project as a startup project</li>
		<li>Build the solution and hit the F5/Start command button</li>
	</ul>
	Building the solution should install all necessary NuGet packages.
</p>
