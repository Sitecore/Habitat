Feature: Compatible Renderings
	As an editor 
	I want to be able to switch between layouts 
	So that I have flexibility in the layout of the pages


@OnlyManual
Scenario: Compatible Renderings_UC1_Check compatible renderings in the Person module
	Given Expirience Editor application is launched
	And en/Modules/Feature/Person page is opened
	And EmployeeTeaser rendering is selected
	When User clicks <Replace with another component> button 
	And Selects EmployeeContactTeaser from <Select a replacement rendering> dialog
	Then Rendering name changed to EmployeeContactTeaser
	And Contact information is added in the bottom of the rendering


@OnlyManual
Scenario: Compatible Renderings_UC2_Data source was not changed
	Given Expirience Editor application is launched
	And en/Modules/Feature/Accounts page is opened
	And PageSidebarLeft rendering is selected
	When User clicks <Replace with another component> button 
	And Selects PageSidebarRight from <Select a replacement rendering> dialog
	Then Rendering name changed to PageSidebarRight
	And SecondaryMenu rendering is now on the right side of the page


@OnlyManual
Scenario: Compatible Renderings_UC3_Check that where is no repeated renderings in the list
	Given Expirience Editor application is launched
	And Modules/Feature/Teasers page is opened
	When User selects <Inner 2 Column 6-6> rendering
	And User clicks <Replace with another component> button 
	Then Following rendering available on the <Select a replacement rendering> dialog
	| Rendering Name     |
	| Inner 2 Column 4-8 |
	| Inner 2 Column 8-4 |
	| Inner 1 Column     |



#Accounts module cases
@Ready
Scenario: Compatible Renderings_UC4_Accounts module
	Then Feature/Accounts module contains incompatible renderings
	| Incompatible renderings |
	| Edit Profile            |
	| Forgot Password         |
	| Login                   |
	| Login Menu              |
	| Register                |



#Demo module cases
@Bug
Scenario: Compatible Renderings_UC5_Demo module
	Then Feature/Demo module contains compatible renderings 
	| Compatible renderings |
	| xDB Contact Details   |
	| xDB Visit Details     |
	And Feature/Demo module contains incompatible renderings
	| Incompatible renderings |
	| Demo Content            |
	| xDB Panel               |



#FAQ module cases
@Ready
Scenario: Compatible Renderings_UC6_FAQ module
	Then Feature/FAQ module contains incompatible renderings
	| Incompatible renderings |
	| FAQ Accordion           |


#Identity module cases
@Ready
Scenario: Compatible Renderings_UC7_Identity module
	Then Feature/Identity module contains incompatible renderings
	| Incompatible renderings |
	| Contact Information     |
	And Feature/Identity module contains compatible renderings
	| Compatible renderings |
	| Copyright             |
	| Logo                  |


#Language module cases
@Ready
Scenario: Compatible Renderings_UC8_Language
	Then Feature/Language module contains incompatible renderings
	| Incompatible renderings |
	| Language menu           |


#Maps module cases
@Ready
Scenario: Compatible Renderings_UC9_Maps module
	Then Feature/Maps module contains incompatible renderings
	| Incompatible renderings |
	| Map                     |


#Media module cases
@Ready
Scenario: Compatible Renderings_UC10_Media module
	Then Feature/Media module contains compatible renderings
	| Compatible renderings |
	| Media Carousel        |
	| Media Gallery         |
	And Feature/Media module contains incompatible renderings
	| Incompatible renderings    |
	| Image with Lightbox        |
	| Page Header Media Carousel |
	| Page Header with Media     |
	| Section with Media         |
	| Video with Lightbox        |


#Metadata module cases
@Ready
Scenario: Compatible Renderings_UC11_Metadata module
	Then Feature/Metadata module contains incompatible renderings
	| Incompatible renderings |
	| HTML Metadata           |


#Multisute module cases
@Ready
Scenario: Compatible Renderings_UC12_Multisute module
	Then Feature/Multisite module contains incompatible renderings
	| Incompatible renderings |
	| Site Menu               |


#Navigation module cases
@Ready
Scenario: Compatible Renderings_UC13_Navigation module	
	Then  Feature/Navigation module contains incompatible renderings
	| Compatible renderings |
	| Breadcrumb            |
	| Primary Menu          |
	| Secondary Menu        |
	| Menu with links       |
	| Navigation Links      |

#News module cases
@Bug
Scenario: Compatible Renderings_UC14_News module
	Then Feature/News module contains compatible renderings
	| Compatible renderings |
	| Latest News           |
	| News List             |
	And Feature/News module contains incompatible renderings
	| Incompatible renderings   |
	| News Content Header Title |
	| News Related Content      |
	| News Article              |
	| News Article Teaser       |



#PageContent module cases
@Bug
Scenario: Compatible Renderings_UC15_PageContent module
	Then Feature/PageContent module contains compatible renderings
	| Compatible renderings     |
	| Page Body                 |
	| Page Content Header Title |
	| Page Image Header         |
	| Page Teaser               |
	And Feature/PageContent module contains incompatible renderings
	| Incompatible renderings |
	| Child Page List         |
	| Page Title              |
	| PageImage               |


#Person module cases
@Ready
Scenario: Compatible Renderings_UC16_Person module
	Then Feature/Person module contains compatible renderings
	| Compatible renderings   |
	| Employee Contact Teaser |
	| Employee Details        |
	| Employee Header Contact |
	| Employee Teaser         |
	And Feature/Person module contains incompatible renderings
	| Incompatible renderings |
	| Employee Header         |
	| Employees Carousel      |
	| Employees List          |
	| Quote                   |







#Search module cases
@Ready
Scenario: Compatible Renderings_UC17_Search module
	Then Feature/Search module contains compatible renderings
	| Compatible renderings   |
	| Paged Search Results    |
	| Search Results          |
	Then Feature/Search module contains incompatible renderings
	| Incompatible renderings |
	| Global Search           |	
	| Search Results Header   |



#Social module cases
@Ready
Scenario: Compatible Renderings_UC18_Social module
	Then Feature/Social module contains incompatible renderings
	| Incompatible renderings      |
	| Facebook Open Graph Metadata |
	| Twitter Feed                 |



#Teasers module cases
@Bug
Scenario: Compatible Renderings_UC19_Teasers module
	Then Feature/Teasers module contains compatible renderings
	| Compatible renderings                 |
	| Call to Action                        |
	| Content Teaser with Image             |
	| Content Teaser with Image and Summary |
	| Content Teaser with Summary           |
	And Feature/Teasers module contains compatible renderings
	| Compatible renderings |
	| Accordion             |
	| Tabs                  |
	And Feature/Teasers module contains compatible renderings
	| Compatible renderings |
	| Jumbotron Carousel    |
	| Teaser Carousel       |
	And Feature/Teasers module contains incompatible renderings
	| Incompatible renderings |
	| Headline                |
	| Call to Action Header   |

	 													
#Project common cases
@Bug												 
Scenario: Compatible Renderings_UC20_Project common
	Then Project/Common module contains compatible renderings
	| Compatible renderings |
	| Article Aside Right   |
	| Article Aside Both    |
	| Article Aside Left    |
	And Project/Common module contains incompatible renderings
	| Incompatible renderings  |
	| Main Navigation Activity |
	| Header Topbar            |
	| Main Navigation          |
	| Page Header              |
	| Section                  |
	| Footer                   |



#Project Section Columns cases
@Bug											 
Scenario: Compatible Renderings_UC21_Project Section Columns
	Then Project/Common/Section Columns module contains compatible renderings
	| Compatible renderings |
	| 2 Column 3-9          |
	| 2 Column 4-8          |
	| 2 Column 8-4          |
	| 2 Column 9-3          |
	And Project/Common/Section Columns module contains compatible renderings
	| Compatible renderings |
	| 3 Column 3-3-6        |
	| 3 Column 6-3-3        |
	And Project/Common/Section Columns module contains incompatible renderings
	| Incompatible renderings |
	| 1 Column                |
	| 2 Column 6-6            |
	| 3 Column 4-4-4          |
	| 4 Column 2-2-4-4        |
	| 4 Column 3-3-3-3        |
	| 6 Column 2-2-2-2-2-2    |



#Project Article Columns cases

@Ready											 
Scenario: Compatible Renderings_UC22_Project Article Columns
	Then Project/Common/Article Columns module contains compatible renderings
	| Compatible renderings |
	| Inner 1 Column        |
	| Inner 2 Column 4-8    |
	| Inner 2 Column 6-6    |
	| Inner 2 Column 8-4    |


