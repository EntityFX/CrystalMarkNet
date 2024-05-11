QuickSort = class(CrystalBenchmarkBase, function(quickSort, writer)
    CrystalBenchmarkBase.init(quickSort, writer, "QuickSort")
end)

QuickSort.MemSize = 2048

function QuickSort:benchImplementation()
    local count = 0
    local temp = QuickSort.MemSize / 4
    local holdrand = 1
    local memory = {}
    local test = {}

    for i = 1, temp, 1 do
        holdrand = bitand(rshift((holdrand * 214013 + 2531011), 16), 0x7fff)
        memory[i] = holdrand
        test[i] = 0
    end

    while not self:isCancelled() do
        for i = 1, Eratosthenes.N, 1 do test[i] = memory[i] end

        table.sort(test)

        count = count + 1
    end

    return count / 4.7 / 25.0
end
