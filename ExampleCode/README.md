In this project there are two consumers that implement an IEventConsumer interface. The first consumer, ***CommunicationPartyLookup*** , first retrieve all communication parties from the 
export endpoint and then keeps a repository updated by polling the events endpoint.
With this pattern, we can store a local copy of all communication parties from the Address Registry.
This class is a highly simplified example and stores all communication parties in a dictionary where the key-value pairs are HerId and a communication party represented as a JsonElement.

The second consumer, ***DummyHealthCareSystem***, simulates a healthcare system that is designed to receive updates via REST, SOAP, or any other protocol they wish to implement. 

This is a highly simplified application and should not be seen as an answer or a guideline for implementation. 
The project is intended solely to provide inspiration on how you can utilize [the REST API](https://cpe.test.grunndata.nhn.no/swagger/index.html) for handling communication party events.