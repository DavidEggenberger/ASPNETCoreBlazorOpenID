# ASPNETCoreBlazorOpenID

Showcasing how an IdentityProvider (Demo Server of IdentityServer) can be used to issue tokens to a Blazor WebAssembly Application. The Auth Token then gets passed to the protected API when the Weatherforecast Component is loaded.
Thats because the BaseAddressAuthorizationMessageHandler specifies such behaviour. The Weatherforecast API then validates the received Auth Token. If it contains the nedded scope (api) the request is authorized. 
