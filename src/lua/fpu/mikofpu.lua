MikoFpu = class(CrystalBenchmarkBase, function(mikofpu, writer)
    CrystalBenchmarkBase.init(mikofpu, writer, "MikoFpu")
end)

MikoFpu.MemSize = 2048

function MikoFpu:benchImplementation()
    local count = 0

    local a = 1.0
    local b = 0.0
    local c = 0.0
    local d = 0.0

    local xch = 0.0

    while not self:isCancelled() do
        b = b + a
        a = a - d

        xch = c
        c = a
        a = xch

        a = a + d

        xch = c
        c = a
        a = xch

        a = a * b

        count = count + 1
        boost = count % 20
    end

    return count / 30000
end
