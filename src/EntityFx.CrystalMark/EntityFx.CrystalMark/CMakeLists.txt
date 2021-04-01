# CMakeList.txt : CMake project for EntityFx.CrystalMark, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Add source to this project's executable.
add_executable (EntityFx.CrystalMark "Main.cpp" "Main.h" "CrystalBenchmarkBase.h" "CrystalBenchmarkBase.cpp" "ALU/Fibonacci.h" "ALU/Fibonacci.cpp" "ALU/Napierian.h" "ALU/Napierian.cpp" "ALU/Eratosthenes.h" "ALU/Eratosthenes.cpp" "ALU/QuickSort.h" "ALU/QuickSort.cpp")

# TODO: Add tests and install targets if needed.
find_package(Threads REQUIRED)
target_link_libraries(EntityFx.CrystalMark PRIVATE Threads::Threads)