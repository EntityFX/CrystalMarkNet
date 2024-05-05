CrystalBenchmarkBase = class(function(b, writer, name)
    b.writer = writer
    b.benchTime = CrystalBenchmarkBase.DefaultTime
    b.cancelled = false
    b.start = 0
    b.name = name
end)

CrystalBenchmarkBase.DefaultTime = 10

function CrystalBenchmarkBase:bench(benchTime)
	if benchTime == nil then
		self.benchTime = CrystalBenchmarkBase.DefaultTime
	end

    self.start = os.clock()
    local res = self:benchImplementation()
    local elapsed = math.floor((os.clock() - self.start))
    local score = res / elapsed
    return score
end

function CrystalBenchmarkBase:benchImplementation()
end

function CrystalBenchmarkBase:isCancelled()
    local endTime = os.clock()
    self.cancelled = endTime - self.start >= CrystalBenchmarkBase.DefaultTime
    return self.cancelled
end
