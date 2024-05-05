require "utils"
require "writer"
require "crystalBenchmarkBase"
require "fibonacci"
require "fft"
require "napierian"

local writer = Writer("Output.log", true, false)

local becnhmarks = {
    Fibonacci(writer, false),
    Napierian(writer, false),
    FFT(writer, false)
}

for index, value in ipairs(becnhmarks) do
    local result = value:bench()
    writer:writeLine("%16s:%8.1f", value.name, result)
end


