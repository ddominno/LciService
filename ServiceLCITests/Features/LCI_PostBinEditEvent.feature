Feature: LCI_PostBinEditEvent

Scenario: Edit a bin that exists in inventory
	Given URL for API is 'dlcis/api/v1/lcis/trans/binedit'
	And Create POST Request
	And Payload is '..\..\DataSources\binedit.json'
	When Execute the Request
	Then Status Code is 'Created'
	And Response Content

Scenario: Edit a bin that is in inventory
	Given the bin appears in inventory
	And Create POST Request
	And the Payload is set for PostBinEditEvent
	And URL for API is 'dlcis/api/v1/lcis/trans/binedit'
	When the Request is executed
	Then Status Code is 'Created'
	And the response property 'Message' contains 'Request processed successfully'

Scenario: Edit a bin that does not exist in inventory
	Given a bin Id that does not exist in inventory
	And Create POST Request
	And the Payload is set for PostBinEditEvent
	And URL for API is 'dlcis/api/v1/lcis/trans/binedit'
	When the Request is executed
	Then Status Code is 'Created'
	And the response property 'Message' contains 'You cannot edit a bin that is not in inventory'