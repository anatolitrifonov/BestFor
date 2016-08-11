Dto classes are returned by services and serve as input to services.

Unfortunately we can not use them in forms with validation because we can not have Dtos referene ResourceService that gives us strings.
If we were to enable this then we would need to mess around iwth somehoe bringing down at least a resource service interface and feed it to 
data annotation attributes.
This is not good and not possible.

So we will use dtos for services only and bring the rest to Models to UI.
Models in UI will have data annotations enabled.

Worst case scenario we convert Model to Dto. Not good but clean separation.
