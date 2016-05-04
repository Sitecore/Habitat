@UI
Feature: Twitter Feed

	In order visitors can follow twitter posts on the website
	As an editor
	I want to be able to add a twitter feed on pages

@NeedImplementation
Scenario: Social_Twitter Feed_UC1_Twitter feed presents on the page
	Given Habitat website is opened on main page
	When Actor scroll down the page
	Then block with following title is present on the page
	| Text                       |
	| Tweets by @sitecorehabitat |


@NeedImplementation
Scenario: Social_Twitter Feed_UC2_Add twitter rendering on the page
	Given Admin user is logged into Habitat
	And Experience Editor application is opened
	And Button <Add a new component> is clicked
	And <Select a Rendering> dialog is opened
	When Admin selects TwitterFeed rendering
	And Admin clicks Select button
	And Admin selects sitecorehabitat datasource
	And Admin clicks OK
	And Admin saves changes on the page
	Then block with following title is present on the page
	| Text                       |
	| Tweets by @sitecorehabitat |


@NeedImplementation
Scenario: Social_Twitter Feed_UC3_Remove twitter rendering from page
	Given Admin user is logged into Habitat
	And Experience Editor application is opened
	And TwitterFeed component is present on the page
	When Admin clicks <Remove component> button on the rendering menu
	Then TwitterFeed is no longer present on the page


@NeedImplementation
Scenario: Social_Twitter Feed_UC4_Edit rendering title
	Given Admin user is logged into Habitat
	And Experience Editor application is opened
	And TwitterFeed component is present on the page
	And FeedTitle contains following value
	| FeedTitle Value            |
	| Tweets by @sitecorehabitat |
	When Admin clicks <Edit related item> button on the rendering menu
	And Edit following value
	| Section name | Field Name | Value               |
	| Twitter Feed | FeedTitle  | Tweets by @sitecore | 
	Then FeedTitle contains following value
	| FeedTitle Value     |
	| Tweets by @sitecore |


@NeedImplementation
Scenario: Social_Twitter Feed_UC5_Make sure latest posts are shown on the page
	Given @sitecorehabitat account is registered on twitter.com
	| Username         | Password   |
	| @sitecorehabitat | Sitecore54 |
	And @sitecorehabitat posted new tweet
	And TwitterFeed based on @sitecorehabitat added on main page
	When user refreshes main page
	Then new tweet appears in the TwitterFeed component on the main page


	@NeedImplementation
Scenario: Social_Twitter Feed_UC6_Add new datasource
	Given Admin user is logged into Habitat
	And Content Editor application is opened
	And /sitecore/content/Shared/Social/Twitter feeds item is selected
	When Admin clicks TwitterFeed button
	And Admin enters <TwitterFeed1> name on Message dialog
	And Admin clicks OK button
	And Admin enters followind data WidgetId field
	| Value              |
	| 641815052882804737 |
	And Admin saves changes on the item
	And Admin opens Experience Editor
	And Admin clicks <Add a new component> button
	And Admin selects TwitterFeed rendering on the <Select a Rendering> dialog 
	And Admin clicks Select button
	Then <TwitterFeed1> datasource available in the list

	@NeedImplementation
Scenario: Social_Twitter Feed_Bug36084_ Place settinfg for configuration number of tweets on Control Properties dialog
	Given Admin user is logged into Habitat
	And Experience Editor application is opened
	And <TwitterFeed> component with <2> tweets on it is present in the bottom of the page
	And <Control Properties> dialog for <TwitterFeed> rendering is opened
	When Admin changes <TweetsToShow> field value to <4>
	And Admin saves changes on the item
	Then <TwitterFeed> component with <4> tweets on it is present in the bottom of the page


