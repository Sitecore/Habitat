Feature: Serialise users and roles


@Ready
Scenario: Serialise users and roles_UC1_Habitat modules roles available in Role Manager
#For manual testing:	Given User is logged to sitecore as Admin
#For manual testing:	When User selects Role Manager
	Then Following roles available
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |
	| modules\Feature Accounts Admin         |
	| modules\Feature Demo Admin             |
	| modules\Feature FAQ Admin              |
	| modules\Feature Identity Admin         |
	| modules\Feature Language Admin         |
#	| modules\Feature Maps Admin             |
	| modules\Feature Media Admin            |
	| modules\Feature Metadata Admin         |
	| modules\Feature Multisite Admin        |
	| modules\Feature Navigation Admin       |
	| modules\Feature News Admin             |
	| modules\Feature PageContent Admin      |
	| modules\Feature Person Admin           |
	| modules\Feature Search Admin           |
	| modules\Feature Social Admin           |
	| modules\Feature Teasers Admin          |
	| modules\Foundation Assets Admin        |
	| modules\Foundation Indexing Admin      |
	| modules\Foundation Multisite Admin     |


@Ready
Scenario: Serialise users and roles_UC2_Non-admin user without modules admin rights
#InitialCondition:
#Package "Serialise users and roles PBI 37273-1.0.zip" should be uploaded.
#Following templates created under /sitecore/content/Habitat/ in this item
#	| Base Templates           |
#	| Standard template        |
#	| _AccountSettings         |
#	| _Interesr                |
#	| _MailTemplate            |
#	| _ProfileSettings         |
#	| _CampaignToken           |
#	| _DemoContent             |
#	| _LinkToken               |
#	| _ProfillingSettings      |
#	| _TextToken               |
#	| _Token                   |
#	| _FAQ                     |
#	| _FAQ Group               |
#	| _Identity                |
#	| _LanguageSettings        |
#	| _MapPoint                |
#	| Map Type                 |
#	| Background Type          |
#	| _HasMedia                |
#	| _HasMediaImage           |
#	| _HasMediaSelector        |
#	| _HasMediaVideo           |
#	| _HasParallaxBackground   |
#	| _MediaSiteExtension      |
#	| _Keyword                 |
#	| _PageMetadata            |
#	| _SiteMetadata            |
#	| _SiteConfiguration       |
#	| _Link                    |
#	| _LinkMenuItem            |
#	| _NavigationRoot          |
#	| _Navigable               |
#	| _NewsArticle             |
#	| _HasPageContent          |
#	| _Employee                |
#	| _Person                  |
#	| _Quote                   |
#	| _SearchResults           |
#	| _TwitterFeed             |
#	| _OpenGraph               |
#	| _Accordeon               |
#	| _TeaserContent           |
#	| Datasource Configuration |
#	| _Site                    |
#	| _SiteSettings            |
#	| _IndexedItem             |
#	| _RenderingAssets         |
#	| _PageAssets              |
#	| FAQ                      |
#	| FAQ Group                |
#	| Interest                 |  
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |              
#For manual testing: User clicks <Lock and Edit> link
	Then habitat\UserRoles has Deny Write access to all available item fields
	#Then All available item fields are disabled


@Ready
Scenario: Serialise users and roles_UC3_Admin user with modules admin rights
#InitialCondition:
#Package "Serialise users and roles PBI 37273-1.0.zip" should be uploaded.
#Following templates created under /sitecore/content/Habitat/ in this item
#	| Base Templates           |
#	| Standard template        |
#	| _AccountSettings         |
#	| _Interesr                |
#	| _MailTemplate            |
#	| _ProfileSettings         |
#	| _CampaignToken           |
#	| _DemoContent             |
#	| _LinkToken               |
#	| _ProfillingSettings      |
#	| _TextToken               |
#	| _Token                   |
#	| _FAQ                     |
#	| _FAQ Group               |
#	| _Identity                |
#	| _LanguageSettings        |
#	| _MapPoint                |
#	| Map Type                 |
#	| Background Type          |
#	| _HasMedia                |
#	| _HasMediaImage           |
#	| _HasMediaSelector        |
#	| _HasMediaVideo           |
#	| _HasParallaxBackground   |
#	| _MediaSiteExtension      |
#	| _Keyword                 |
#	| _PageMetadata            |
#	| _SiteMetadata            |
#	| _SiteConfiguration       |
#	| _Link                    |
#	| _LinkMenuItem            |
#	| _NavigationRoot          |
#	| _Navigable               |
#	| _NewsArticle             |
#	| _HasPageContent          |
#	| _Employee                |
#	| _Person                  |
#	| _Quote                   |
#	| _SearchResults           |
#	| _TwitterFeed             |
#	| _OpenGraph               |
#	| _Accordeon               |
#	| _TeaserContent           |
#	| Datasource Configuration |
#	| _Site                    |
#	| _SiteSettings            |
#	| _IndexedItem             |
#	| _RenderingAssets         |
#	| _PageAssets              |
#	| FAQ                      |
#	| FAQ Group                |
#	| Interest                 | 	
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |
#For manual testing: User clicks <Lock and Edit> link	             
	Then habitat\UserRoles has  Write access to all available item fields


@Ready
Scenario: Serialise users and roles_UC4_Admin user with multisite admin rights
#InitialCondition:
#Package "Serialise users and roles PBI 37273-1.0.zip" should be uploaded.
#Following templates created under /sitecore/content/Habitat/ in this item
#	| Base Templates           |
#	| Standard template        |
#	| _AccountSettings         |
#	| _Interesr                |
#	| _MailTemplate            |
#	| _ProfileSettings         |
#	| _CampaignToken           |
#	| _DemoContent             |
#	| _LinkToken               |
#	| _ProfillingSettings      |
#	| _TextToken               |
#	| _Token                   |
#	| _FAQ                     |
#	| _FAQ Group               |
#	| _Identity                |
#	| _LanguageSettings        |
#	| _MapPoint                |
#	| Map Type                 |
#	| Background Type          |
#	| _HasMedia                |
#	| _HasMediaImage           |
#	| _HasMediaSelector        |
#	| _HasMediaVideo           |
#	| _HasParallaxBackground   |
#	| _MediaSiteExtension      |
#	| _Keyword                 |
#	| _PageMetadata            |
#	| _SiteMetadata            |
#	| _SiteConfiguration       |
#	| _Link                    |
#	| _LinkMenuItem            |
#	| _NavigationRoot          |
#	| _Navigable               |
#	| _NewsArticle             |
#	| _HasPageContent          |
#	| _Employee                |
#	| _Person                  |
#	| _Quote                   |
#	| _SearchResults           |
#	| _TwitterFeed             |
#	| _OpenGraph               |
#	| _Accordeon               |
#	| _TeaserContent           |
#	| Datasource Configuration |
#	| _Site                    |
#	| _SiteSettings            |
#	| _IndexedItem             |
#	| _RenderingAssets         |
#	| _PageAssets              |
#	| FAQ                      |
#	| FAQ Group                |
#	| Interest                 |
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |                 
#For manual testing: When User navigates to Content Editor with user
#	| User name  | Password |
#	| UserRoles3 | u        |
#For manual testing: User clicks <Lock and Edit> link  
    Then habitat\UserRoles has  Write access to following item fields
    | Item name           |
  	| DatasourceLocation  |
  	| DatasourceTemplate  |

