Feature: LCI_PostBinMoveEvent

Scenario: POST Bin Move Event
	Given URL for API is 'dlcis/api/v1/lcis/trans/binmove'
	And Create POST Request
	And Payload is '..\..\DataSources\binmove.json'
	When Execute the Request
	Then Status Code is 'OK'
	And Response Content

Scenario: Move a bin that exists in inventory
	Given the bin appears in inventory
	And Create POST Request
	And the Payload is set for PostBinMoveEvent
	And URL for API is 'dlcis/api/v1/lcis/trans/binmove'
	When the Request is executed
	Then Status Code is 'OK'
	And the response property 'Message' contains 'Request Processed Successfully'

Scenario: Move a bin not in inventory
	Given a bin Id that does not exist in inventory
	And Create POST Request
	And the Payload is set for PostBinMoveEvent
	And URL for API is 'dlcis/api/v1/lcis/trans/binmove'
	When the Request is executed
	Then Status Code is 'BadRequest'
	And the response property 'Message' contains 'does not appear to be in inventory'