require "utils/utils"
require "utils/writer"
require "crystalBenchmarkBase"
require "alu/fibonacci"
require "alu/napierian"
require "alu/eratosthenes"
require "alu/quicksort"
require "fpu/mikofpu"
require "fpu/fft"
require "fpu/mandelbrot"

local writer = Writer("Output.log", true, false)

local becnhmarks = {
    MikoFpu(writer, false),
    Fibonacci(writer, false),
    Napierian(writer, false),
    Eratosthenes(writer, false),
    QuickSort(writer, false),
    FFT(writer, false),
    Mandelbrot(writer, false)
}

for index, value in ipairs(becnhmarks) do
    local result = value:bench()
    writer:writeLine("%16s:%8.1f", value.name, result)
end


