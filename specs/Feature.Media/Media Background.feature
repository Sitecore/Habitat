Feature: Media Background

	

@NeedImplementation
Scenario: Media Background_Parallax_UC1_Image background with paralax 
	Given Habitat website is opened on Main Page
	When Actor opens Parallax page
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                  | data-multibackground-layer-0-attachment | data-multibackground-layer-0-parallaxspeed |
	| parallax | /-/media/Habitat/Images/Square/Habitat-019-square.jpg | parallax                                | 1.0                                        |

@NeedImplementation
Scenario: Media Background_Parallax_UC2_Image background without paralax
	Given Habitat website is opened on Main Page
	When Actor opens Parallax page
#Please don't forget to change image name to new one when it's available. 
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                  | data-multibackground-layer-0-attachment |
	| parallax | /-/media/Habitat/Images/Square/Habitat-019-square.jpg | static                                  |
		

@NeedImplementation
Scenario: Media Background_Parallax_UC3_Video background with paralax 
	Given Habitat website is opened on Main Page
	When Actor opens Parallax page
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                                        | data-multibackground-layer-0-attachment |data-multibackground-layer-0-parallaxspeed |
	| parallax | /-/media/Habitat/Videos/Sitecore-Experience-Platform-on-Microsoft-Azure.mp4 | parallax                                |1.0                                        |

@NeedImplementation
Scenario: Media Background_Parallax_UC4_Video background without paralax 
	Given Habitat website is opened on Main Page
	When Actor opens Parallax page
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                                        | data-multibackground-layer-0-attachment |
	| parallax | /-/media/Habitat/Videos/Sitecore-Experience-Platform-on-Microsoft-Azure.mp4 | static                                  | 


@NeedImplementation
Scenario: Media Background_Parallax_UC5_Change from parallax to static
	Given Value set to item field
	| ItemPath                                                         | FieldName           | FieldValue |
	| /sitecore/content/Habitat/Global/Media/Parallax/Parallax Picture | Is Parallax Enabled | 0          |
	When Actor opens Parallax page
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                  | data-multibackground-layer-0-attachment |
	| parallax | /-/media/Habitat/Images/Square/Habitat-019-square.jpg | static                                  |
		 	

@NeedImplementation
Scenario: Media Background_Parallax_UC6_Change from static to parallax 
	Given Value set to item field
	| ItemPath                                                                  | FieldName           | FieldValue |
	| /sitecore/content/Habitat/Global/Media/Parallax/Static Background Picture | Is Parallax Enabled | 1          |
	When Actor opens Parallax page
#Please don't forget to change image name to new one when it's available.
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                  | data-multibackground-layer-0-attachment |data-multibackground-layer-0-parallaxspeed |
	| parallax | /-/media/Habitat/Images/Square/Habitat-019-square.jpg | static                                  |1.0                                        |


@NeedImplementation
Scenario: Media Background_Parallax_UC6_Change parallax speed  
	Given Value set to item field
	| ItemPath                                                         | FieldName      | FieldValue |
	| /sitecore/content/Habitat/Global/Media/Parallax/Parallax Picture | Parallax Speed | 1.5        |
	When Actor opens Parallax page
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                  | data-multibackground-layer-0-attachment |data-multibackground-layer-0-parallaxspeed |
	| parallax | /-/media/Habitat/Images/Square/Habitat-019-square.jpg | static                                  |1.5                                        |



@NeedImplementation
Scenario: Media Background_Parallax_UC8_Inverted parallax 
	Given Habitat website is opened on Main Page
	When Actor opens Parallax page
#Please don't forget to change image name to new one when it's available.
	Then Following parallax control presents on page
	| Class    | data-multibackground-layer-0-url-mp4                  | data-multibackground-layer-0-attachment | data-multibackground-layer-0-parallaxspeed |
	| parallax | /-/media/Habitat/Images/Square/Habitat-019-square.jpg | parallax                                | -1.0                                       |


@NeedImplementation
Scenario: Media Background_Parallax_UC9_Paralax template is available as Insert Options
	Given Actor has opened Habitat Content editor
	When User selects /sitecore/content/Habitat/Global/Media/Parallax item
	Then Following sub-items are available as Insert Options
	| Insert option |
	| Parallax      | 


