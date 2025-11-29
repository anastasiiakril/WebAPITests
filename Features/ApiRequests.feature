Feature: API requests



@tag1
Scenario: test get request
	Given connected
	And create get request
	When send request
	Then response is success



@tag1
Scenario: test create request
	Given connected
	And create create request
	When send request
	Then response create is success

	
@tag1
Scenario: test update request
	Given connected
	And create auth token
	And create update request
	When send request
	Then response update is success

	
@tag1
Scenario: test delete request
	Given connected
	And create auth token
	And create delete request
	When send request
	Then response delete is success
