@UI
Feature: Twitter Feed

	In order visitors can follow twitter posts on the website
	As an editor
	I want to be able to add a twitter feed on pages

@Ready
Scenario: Social_Twitter Feed_UC1_Twitter feed presents on the page
	Given Social Page is opened
	Then Block with following title is present on the page
	| Text                       |
	| Tweets by @sitecorehabitat |


@OnlyManual
Scenario: Social_Twitter Feed_UC2_Add twitter rendering on the page
	Given Experience Editor is opened on Social Page
	And Button <Add a new component> is clicked
	And <Select a Rendering> dialog is opened
	When Admin selects TwitterFeed rendering
	And Admin clicks Select button
	And Admin selects sitecorehabitat datasource
	And Admin clicks OK
	And Admin saves changes on the page
	Then Block with following title is present on the page
	| Text                       |
	| Tweets by @sitecorehabitat |


@OnlyManual
Scenario: Social_Twitter Feed_UC3_Remove twitter rendering from page
	Given Experience Editor is opened on Social Page
	And TwitterFeed component is present on the page
	When Admin clicks <Remove component> button on the rendering menu
	Then TwitterFeed is no longer present on the page


@Ready
Scenario: Social_Twitter Feed_UC4_Edit rendering title
	Given Value set to item field
	| ItemPath                                                | FieldName | FieldValue          |
	| /sitecore/content/Habitat/Global/Social/sitecorehabitat | FeedTitle | Tweets by @sitecore |
	When Social Page is opened
	Then Block with following title is present on the page
	| FeedTitle Value     |
	| Tweets by @sitecore |
	And Value set to item field
	| ItemPath                                                | FieldName | FieldValue                 |
	| /sitecore/content/Habitat/Global/Social/sitecorehabitat | FeedTitle | Tweets by @sitecorehabitat |


@OnlyManual
Scenario: Social_Twitter Feed_UC5_Make sure latest posts are shown on the page
	Given @sitecorehabitat account is registered on twitter.com
	| Username         | Password   |
	| @sitecorehabitat | Sitecore54 |
	And @sitecorehabitat posted new tweet
	And TwitterFeed based on @sitecorehabitat added on main page
	When user refreshes main page
	Then new tweet appears in the TwitterFeed component on the main page


	@Ready
Scenario: Social_Twitter Feed_UC6_Add new datasource
	Given Following items should be added
	| ItemPath     | ParentItemPath                           | templateItemPath                                                     |
	| Twitter Feed | /sitecore/content/Habitat/Global/Social/ | /sitecore/templates/Project/Common/Content Types/Social/Twitter Feed | 
	And Value set to item field
	| ItemPath                                             | fieldName | fieldValue         |
	| /sitecore/content/Habitat/Global/Social/Twitter Feed | WidgetId  | 641815052882804737 |
	And Experience Editor is opened on Social Page
	When User selects Twitter placeholder
	And User clicks Associate a content item with this component. button on scChromeToolbar undefined
	Then Following items present in datasource tree
	| Item name       |
	| sitecorehabitat |
	| Twitter Feed    |

	@InProgress
Scenario: Social_Twitter Feed_Bug36084_ Place settinfg for configuration number of tweets on Control Properties dialog
	Given Control properties were defined for item
	| ItemPath                                              | ControlId                              | Type         | Value |
	| /sitecore/content/Habitat/Home/Modules/Feature/Social | {0CF58F14-D0F8-4C41-9896-3D69048A0B19} | TweetsToShow | 4     |
	When Social Page is opened
	Then TwitterFeed component contains 4 tweets 


