Feature: Video in Media Carousel
	

@NeedIMplementation
Scenario: Video in Media Carousel_UC1_Review default video item
	Given Habitat website is opened on Main Page
	When User opens Home page 
	Then Following video presents
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore-Experience.mp4 |
	


@NeedIMplementation
Scenario: Video in Media Carousel_UC2_New video was added to Carousel
	Given Habitat Content Editor was opened
	And <Sitecore CMS.mp4> video should be added to Media library Habitat, Saved and Published
	| Video test source                                                             |
	| \Habitat\src\Domain\Navigation\specs\TestData\Media Carousel\Sitecore CMS.mp4 |	
#Media library Habitat video path: /sitecore/media library/Habitat/Videos
	And <Sitecore CMS.mp4> thumbnail should be added to Media library Habitat, Saved and Published
	| Image test source                                                            |
	| \Habitat\src\Domain\Navigation\specs\TestData\Media Carousel\HabitatTest.jpg |
#Media library Habitat image path: /sitecore/media library/Habitat/Images/Wide	 
	When User starts Video item creation under Habitat Shared Media
#Habitat Shared Media path: /sitecore/content/Shared/Media
	And User inserts <Sitecore CMS.mp4 path> into Link to video: field
	And User inserts <Habitat-001-wide path> image into Thumbnail field
	And User clicks Save button on Content Editor Ribbon
	And User selects Carousel item 
#Carousel item path: /sitecore/content/Shared/Media/Carousel
	And User moves <Sitecore CMS.mp4> item to Selected section 
#Double-click on item name
	And User clicks Save button on Content Editor Ribbon
	And User publishes /sitecore/content/Shared/Media item with sub-items
	And User opens Home page
	Then Carousel element presents at Habitat Home page 
	And Following video presents
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore-Experience.mp4 |
	| /-/media/Habitat/Videos/Sitecore CMS.mp4        |
	And Following thumbnail presents
	| Thumbnail path                               |
	| /-/media/Habitat/Images/Wide/HabitatTest.jpg |	 	   


@NeedIMplementation
Scenario: Video in Media Carousel_UC3_New video was added to Carousel_Link to video field is empty and thumbnail filled
	Given Habitat Content Editor was opened
	And <Sitecore CMS.mp4> thumbnail should be added to Media library Habitat, Saved and Published
	| Image test source                                                            |
	| \Habitat\src\Domain\Navigation\specs\TestData\Media Carousel\HabitatTest.jpg |
	When User starts Video item creation under Habitat Shared Media
#Media library Habitat image path: /sitecore/media library/Habitat/Images/Wide
	And User inserts <Habitat-001-wide path> image into Thumbnail field
	And User clicks Save button on Content Editor Ribbon
	And User selects Carousel item 
#Carousel item path: /sitecore/content/Shared/Media/Carousel
	And User moves <Sitecore CMS.mp4> item to Selected section 
#Double-click on item name
	And User clicks Save button on Content Editor Ribbon
	And User publishes /sitecore/content/Shared/Media item with sub-items
	And User opens Home page
	Then Carousel element presents at Habitat Home page 
	And Following video presents
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore-Experience.mp4 |
	And Following video absents
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore CMS.mp4        |
	And Following thumbnail presents
	| Thumbnail path                               |
	| /-/media/Habitat/Images/Wide/HabitatTest.jpg | 	   

@NeedIMplementation
Scenario: Video in Media Carousel_UC4_New video was added to Carousel_Link to video field is empty and thumbnail empty
	Given Habitat Content Editor was opened
	And <Sitecore CMS.mp4> video should be added to Media library Habitat, Saved and Published
	| Video source                                                                  |
	| \Habitat\src\Domain\Navigation\specs\TestData\Media Carousel\Sitecore CMS.mp4 |
#Media library Habita path: /sitecore/media library/Habitat/Videos 
	When User starts Video item creation under Habitat Shared Media
#Habitat Shared Media path: /sitecore/content/Shared/Media
	And User clicks Save button on Content Editor Ribbon
	And User selects Carousel item 
#Carousel item path: /sitecore/content/Shared/Media/Carousel
	And User moves <Sitecore CMS.mp4> item to Selected section 
#Double-click on item name
	And User clicks Save button on Content Editor Ribbon
	And User publishes /sitecore/content/Shared/Media item with sub-items
	And User opens Home page
	Then Carousel element presents at Habitat Home page 
	And Following video presents
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore-Experience.mp4 | 	   
	And Following video absents
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore CMS.mp4        |
	And Following thumbnail absents
	| Thumbnail path                               |
	| /-/media/Habitat/Images/Wide/HabitatTest.jpg | 

@NeedIMplementation
Scenario: Video in Media Carousel_UC5_New video was added to Carousel_Link to video field is filled and thumbnail empty
	Given Habitat Content Editor was opened
	And <Sitecore CMS.mp4> video should be added to Media library Habitat, Saved and Published
	| Video source                                                                                                               |
	| \Habitat\src\Domain\Navigation\specs\TestData\Media Carousel\Sitecore CMS.mp4 |
#Media library Habita path: /sitecore/media library/Habitat/Videos 
	When User starts Video item creation under Habitat Shared Media
#Habitat Shared Media path: /sitecore/content/Shared/Media
	And User inserts <Sitecore CMS.mp4 path> into Link to video: field
	And User clicks Save button on Content Editor Ribbon
	And User selects Carousel item 
#Carousel item path: /sitecore/content/Shared/Media/Carousel
	And User moves <Sitecore CMS.mp4> item to Selected section 
#Double-click on item name
	And User clicks Save button on Content Editor Ribbon
	And User publishes /sitecore/content/Shared/Media item with sub-items
	And User opens Home page
	Then Carousel element presents at Habitat Home page 
	And Following video presents
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore-Experience.mp4 |
	| /-/media/Habitat/Videos/Sitecore CMS.mp4        |	 	   
	And Following thumbnail absents
	| Thumbnail path                               |
	| /-/media/Habitat/Images/Wide/HabitatTest.jpg | 

@NeedIMplementation
Scenario: Video in Media Carousel_UC6_Video was removed from carousel  
	Given Habitat Content Editor was opened
	When And User selects Carousel item 
#Carousel item path: /sitecore/content/Shared/Media/Carousel
	And User removes <Sitecore-Experience.mp4> item from Selected section
#Double-click on item name
	And User clicks Save button on Content Editor Ribbon
	And User publishes /sitecore/content/Shared/Media item with sub-items
	And User opens Home page
	Then Carousel element presents at Habitat Home page
	And Following video absents 
	| Video path                                      |
	| /-/media/Habitat/Videos/Sitecore-Experience.mp4 |

	
	


