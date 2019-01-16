Feature: LCI_PostBinIssuanceEvent

Scenario: Issue a bin Id
	Given URL for API is 'dlcis/api/v1/lcis/trans/issuance'
	And Create POST Request
	And Payload is '..\..\DataSources\POSTBinIssuanceEvent.json'
	When Execute the Request
	Then Status Code is 'Created'
	And Response Content 

Scenario: Issue a bin Id that does not exist in inventory
	Given a bin Id that does not exist in inventory
	And Create POST Request
	And the Payload is set for PostBinIssuance
	And URL for API is 'dlcis/api/v1/lcis/trans/issuance'
	When the Request is executed
	Then Status Code is 'BadRequest'
	And the response property 'Message' contains 'does not exist in inventory. You cannot issue it'

	#DTAP need a way to retrieve where bin originated from
Scenario: Issue a bin to a ranchNumber it did not originate from
	Given a binId has been created
	And the ranch used is not where the bin originated from
	And Create POST Request
	And the Payload is set for PostBinIssuance
	And URL for API is 'dlcis/api/v1/lcis/trans/issuance'
	When the Request is executed
	Then Status Code is 'BadRequest'
	And the response property 'message' contains 'you cannot issue bins to a ranch it does not belong to'
