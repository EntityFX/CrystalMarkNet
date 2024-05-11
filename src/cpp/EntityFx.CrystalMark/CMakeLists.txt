# CMakeList.txt : CMake project for EntityFx.CrystalMark, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Add source to this project's executable.
add_executable (${PROJECT_NAME} "Main.cpp" "Main.h" "CrystalBenchmarkBase.h" "CrystalBenchmarkBase.cpp" 
"ALU/Fibonacci.h" "ALU/Fibonacci.cpp" "ALU/Napierian.h" "ALU/Napierian.cpp" "ALU/Eratosthenes.h" "ALU/Eratosthenes.cpp" "FPU/MikoFpu.cpp" "ALU/QuickSort.h" "ALU/QuickSort.cpp"
"FPU/MikoFpu.cpp" "FPU/MikoFpu.h" "FPU/Mandelbrot.h" "FPU/Mandelbrot.cpp" "FPU/FFT.h" "FPU/FFT.cpp" "FPU/RandMeanSS.h" "FPU/RandMeanSS.cpp")

# TODO: Add tests and install targets if needed.
find_package(Threads REQUIRED)
target_link_libraries(${PROJECT_NAME} Threads::Threads)

IF(CMAKE_COMPILER_IS_GNUCC)
    set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_DEBUG} -O3")
    set(CMAKE_C_FLAGS_RELEASE "${CMAKE_C_FLAGS_DEBUG} -O3")
ENDIF(CMAKE_COMPILER_IS_GNUCC)