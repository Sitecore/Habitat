@UI
Feature: Сapability for search results to render using data type specific renderings
	

@mytag
Scenario: Сapability for search results to render using data type specific renderings_UC1_
	Given Habitat website is opened on Main Page
	When User clicks Search button
	And User enters <xp> into search text field
	And User presses Enter button 
	Then Following specific renderings were added to News page search results
	| Results title                                             | Specific rendering           |
	| New praise for world class experience management platform | August 24, 2015              |
	| Sitecore XP rated Best In Class - again                   | September 24, 2015           |
	And Following specific renderings were added to Person page search results
	| Results title | Specific rendering           |
	| John Doe      | +1 202 555 0162              |
	| John Doe      | +1 202 555 0148              |
	| John Doe      | john.doe@sitecorehabitat.com |


