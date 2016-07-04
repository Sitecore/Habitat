@UI
Feature: Paged Search Result
	In order to find the content I am searching for
	As a website visitor
	I want to be able to browse through multiple pages of search results

	@NeedImplementation
Scenario: Search_Paged Search Results_UC1_Open search result page
	Given Term <T1> was presented on 2 pages
	And User has entered <T1> into search box
	When User clicks search link
	Then Title <Search> presents on the page
	And URL ends with Search?query=T1


	@NeedImplementation
Scenario: Search_Paged Search Results_UC2_No search results
	Given Habitat website is opened on Main Page
	When User user hovers over <search-icon>
	And User clicks search link
	Then Title <Search> presents on the page
	And following text is under page title
	|Text             |
	|No results found |
	And search box on the right side of the page is empty


	@NeedImplementation
Scenario: Search_Paged Search Results_UC3_Change default number of results on the page
	Given Admin user is logged into Habitat
	And Experience Editor application is opened 
	And Search Results page is opened
	And <Control Properties> dialog for <PagedSearchResults> rendering is opened
	When Admin changes <ResultsOnPage> field value to <5>
	And Admin publishes site
	And Admin logs out from Habitat
	And Actor hovers over <search-icon>
	And Actor enters <T1> search term
	And Actor clicks search link
	Then number of results on the <Search> page is equal to <5>


	@NeedImplementation
Scenario: Search_Paged Search Results_UC4_Change default number of pages to show
	Given Term <T2> was presented on 5 pages
	And Admin user is logged into Habitat
	And Experience Editor application is opened 
	And Search Results page is opened
	And <Control Properties> dialog for <PagedSearchResults> rendering is opened
	When Admin changes <PagesToShow> field value to <3>
	And Admin publishes site
	And Admin logs out from Habitat
	And Actor hovers over <search-icon>
	And Actor enters <T2> search term
	And Actor clicks search link
	Then number of pages on the <Search> page is equal to <3>
  
	@NeedImplementation
Scenario: Search_Paged Search Results_UC5_Next button
	Given Term <T3> was presented on 3 pages
	And User has entered <T3> into search box
	And User clicked search link
	When User clicks <Next> link
	Then 2d page is highlighted and search results is updated


	@NeedImplementation
Scenario: Search_Paged Search Results_UC6_Previous button
	Given Term <T3> was presented on 3 pages
	And Search for <T3> is performed
	And <2> page is highlighted in the paged search results
	When User clicks <Prev> link
	Then 1st page is highlighted and search results is updated


	@NeedImplementation
Scenario: Search_Paged Search Results_UC7_Check that the last page is last
	Given Term <T3> was presented on 3 pages
	And User has entered <T3> into search box
	And User clicked search link
	When User highlights 3d page in the paged search results
	And User clicks <Next> link 
	Then <Next> link is disabled


	@NeedImplementation
Scenario: Search_Paged Search Results_UC8_Check that the first page is first
	Given Term <T3> was presented on 3 pages
	And User has entered <T3> into search box
	And User clicked search link
	When User clicks <Prev> link
	Then <Prev> link is disabled

