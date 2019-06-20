Main = Main or {}

Main.Init = function()
    require("Common/XObject")
    
    local initScript = require("TestLua")
    initScript.Start()
end

Main.Init()