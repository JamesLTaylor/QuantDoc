Components
==========

A Visual Studio Plugin
----------------------

This will generate the previews and allow developers to click through the links to the 
editor for restructured text. At the moment the restructured text editor is Visual Studio code.

A Visual Studio Code Plugin
---------------------------

This will keep track of references from the code to the documentation and a count a links back to 
the places where the code makes the reference.

A Browser Plugin/Sphinx Extension
---------------------------------

To allow generated documentation to link back the source in GitHub rather than the source
editor as happens while the docs are being written.

A Sphinx Plugin
---------------

To generate tables and graphs from C# code that runs during document compile. Importantly this
should only be required when the user requests it since tables and graphs in validation could 
take a long time to generate (for example multiple simulations may be run to test Monte Carlo 
convergence).