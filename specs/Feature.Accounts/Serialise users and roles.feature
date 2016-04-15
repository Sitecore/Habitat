Feature: Serialise users and roles


@Ready
Scenario: Serialise users and roles_UC1_Habitat modules roles available in Role Manager
	
	Then Following roles available
  | Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |
	| modules\Feature Accounts Admin         |
	| modules\Feature Demo Admin             |
	| modules\Feature FAQ Admin              |
	| modules\Feature Identity Admin         |
	| modules\Feature Language Admin         |
	| modules\Feature Maps Admin             |
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
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |              

  Then habitat\UserRoles has Deny Write access to all available item fields
	#Then All available item fields are disabled


@Ready
Scenario: Serialise users and roles_UC3_Admin user with modules admin rights
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |             

#Then All available item fields are enabled
  Then habitat\UserRoles has  Write access to all available item fields


@Ready
Scenario: Serialise users and roles_UC4_Admin user with multisite admin rights
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |                 
	When User navigates to Content Editor with user
	| User name  | Password |
	| UserRoles3 | u        |
#For manual testing: User clicks <Lock and Edit> link
	Then Only following item fields are enabled and all other are disabled
	| Item name            |
	| Datasource Location  |
	| Datasource Template  |


