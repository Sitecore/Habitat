Feature: Buckets
	As a news editor 
	I want an easy structure for my news 
	so that I dont have to worry about where to put articles.

@NeedImplementation
Scenario: News_Buckets_UC1_Create a new news article in a news folder_Content Editor
	Given Content Editor application is opened
	And Admin user is logged in
	And User selects following item in the Content Tree 
	| Item Path                                                |
	| /sitecore/content/Habitat/Home/Modules/Feature/News/News |
	When User selects <News Article> in the Insert chunk of Home tab
	And clicks OK on the Message window
	Then news article is automatically placed in a bucket with following item path ia added
	| Item Path                                                                                           |
	| /sitecore/content/Habitat/Home/Modules/Feature/News/News/year/month/date/hours/minutes/News Article |


@NeedImplementation
Scenario: News_Buckets_UC2_Create a new news article in a news folder_Experience Editor
	Given Experience Editor application is opened 
	And Admin user is logged in
	And Home/Modules/Feature/News/News page is opened
	When User clicks <Insert page> button on the ribbon
	And User selects <News Article>
	And clicks OK
	Then breadcrumbs contains following path <Home>Modules>Feature>News>News>year>month>date>hours>minutes>News Article>


@NeedImplementation
Scenario: News_Buckets_UC3_Create new news folder
	Given Content Editor application is opened
	And Admin user is logged in
	And Content Tree is expanded 
	And following item is selected
	| Item Path                                           |
	| /sitecore/content/Habitat/Home/Modules/Feature/News |
	And <Insert from Template> dialog is opened
	When User selects </Project/Habitat/Page Types/News List> template
	And clicks Insert button
	And User inserts new <News Article>
	Then new bucketed folder created under News


@NeedImplementation
Scenario: News_Buckets_UC4_Publish news article placed in a bucket
	Given Content Editor application is opened
	And Admin user is logged in
	And User selects following item in the Content Tree 
	| Item Path                                                |
	| /sitecore/content/Habitat/Home/Modules/Feature/News/News |
	When User selects <News Article> in the Insert chunk of Home tab
	And clicks OK on the Message window
	And User click Publish Site on the ribbon
	And User opens following item in Experience Editor mode
	| Item Path                                                                                           |
	| /sitecore/content/Habitat/Home/Modules/Feature/News/News/year/month/date/hours/minutes/News Article |
	And Admin logs out from sitecore backend
	And follows the link http://habitat.test5ua1.dk.sitecore.net/Modules/Feature/News/News
	Then new empty news article is in the bottom of the page
	