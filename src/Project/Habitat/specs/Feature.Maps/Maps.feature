@UI
Feature: Maps
	

@NeedImplementation
Scenario: Maps_UC1_Add new map
	Given New item was created 
	| Under item path                             | Template ID                          | Item name   |
	| /sitecore/content/Habitat/Global/Map Points | 067EC866-B3FB-485B-BB49-6151FB086E78 | Custom Maps |
	And New item was created 
	| Under item path                                         | Template ID                          | Item name    | Addres         | Location                             |
	| /sitecore/content/Habitat/Global/Map Points/Custom Maps | 11C28D16-9B88-456E-A42B-D4B5A82867E3 | Old Trafford | Manchester, GB | 53.46165999999999,-2.271706999999992 |
	| /sitecore/content/Habitat/Global/Map Points/Custom Maps | 11C28D16-9B88-456E-A42B-D4B5A82867E3 | Wembley      | London, GB     | 51.550501,-0.3048409000000447        |
	When User defines final layout on page
	| Item path                                           | Control | Data Source                                 |
	| /sitecore/content/Habitat/Home/Modules/Feature/Maps | Map     | /sitecore/content/Habitat/Global/Map Points |
	Then Custom Maps present on Maps page




