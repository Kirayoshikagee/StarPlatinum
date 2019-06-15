function XObject(superObj, path)
    local obj = {}
    obj.super = superObj
    obj.classpath = path
    obj.objType = "XObject"

    return obj
end