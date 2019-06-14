local TestLua = {}

TestLua.gameObject = nil
TestLua.transform = nil

function TestLua.Start(csGameObject, csTransform)
    TestLua.gameObject = csGameObject
    TestLua.transform = csTransform

    print(TestLua.transform.localPosition.x .. ":" .. TestLua.transform.localPosition.y .. ":" .. TestLua.transform.localPosition.z)
    print(TestLua.gameObject.name)
end

function TestLua.OnDestroy()
    TestLua.gameObject = nil
    TestLua.transform = nil
end

function TestLua:SayHello()
    print("TestLua:Hello xlua")
end

return TestLua