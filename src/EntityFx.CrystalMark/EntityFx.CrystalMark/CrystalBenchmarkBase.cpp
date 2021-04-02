#include "CrystalBenchmarkBase.h"
#include <iostream>
#include <tuple>

int CrystalBenchmarkBase::bench(int threads) {
	int result = this->bench_all(threads);
	return result;
}

int CrystalBenchmarkBase::bench_all(int threads)
{
	std::future<int>* futures = new std::future<int>[threads];
	std::atomic_bool cancellation_token;


	std::vector<int> readyFutures;

	for (int i = 0; i < threads; ++i) {
		futures[i] = std::async(std::launch::async, &CrystalBenchmarkBase::bench_implementation, this, std::ref(cancellation_token));
	}

	std::chrono::milliseconds span(10000);

	while (futures[0].wait_for(span) == std::future_status::timeout) {
		cancellation_token = true;
	}

	int validFutures = 0;

	while (validFutures < threads) {
		for (int i = 0; i < threads; ++i) {
			if (futures[i].valid()) {
				validFutures++;
				continue;
			}
		}
	}

	for (int i = 0; i < threads; ++i) {
		readyFutures.push_back(futures[i].get());
	}
	
	cancellation_token = false;

	int result = 0;

	for (auto& n : readyFutures)
		result += n;

	delete [] futures;
	return result;
}
