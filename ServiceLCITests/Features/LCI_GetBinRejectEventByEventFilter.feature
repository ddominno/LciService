Feature: LCI_GetBinRejectEventByEventFilter
	Scenario: Test to get bin rejects filtered by GrowerNbr
	Given I have a Bin request 
	When I pass a grower Id existing
		And Execute the Request
	Then I should get status code 'OK'
		And a response with valid event type

	Scenario: Test to get bin rejects filtered by GrowerNbr that does not exist
	Given I have a Bin request 
	When I pass a grower Id not existing 
		And Execute the Request
	Then I should get status code 'OK'
		And the response content is empty