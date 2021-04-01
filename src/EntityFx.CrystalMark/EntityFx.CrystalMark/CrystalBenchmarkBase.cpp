#include "CrystalBenchmarkBase.h"
#include <iostream>
#include <tuple>

int CrystalBenchmarkBase::bench(int threads) {
	std::atomic_bool cancellation_token;
	//std::function<int(std::atomic_bool&)> callback = std::bind(&CrystalBenchmarkBase::bench_implementation, this, std::ref(cancellation_token));

	int result = this->bench_all(threads);
	return result;
}

int CrystalBenchmarkBase::bench_all(int threads)
{
	std::vector<std::future<int>> futures;
	std::vector<std::reference_wrapper< std::atomic_bool>> cancellation_tokens;

	std::vector<int> readyFutures;

	for (int i = 0; i < threads; ++i) {
		std::atomic_bool cancellation_token;

		std::reference_wrapper< std::atomic_bool> ct = std::ref(cancellation_token);

		cancellation_tokens.push_back(ct);

		futures.push_back(
			std::async(std::launch::async, &CrystalBenchmarkBase::bench_implementation, this, ct));
	}

	std::chrono::milliseconds span(10000);

	for (unsigned i = 0; i < futures.size(); i++) {
		std::future<int>& future = futures.at(i);
		if (future.valid()) {
			while (future.wait_for(span) == std::future_status::timeout) {
				std::atomic_bool& cancellation_token = cancellation_tokens.at(i);
				cancellation_token = true;
			}
		}

	}


	for (auto& future : futures) {
		readyFutures.push_back(future.get());
	}

	int result = 0;

	for (auto& n : readyFutures)
		result += n;

	return result;
}

//int CrystalBenchmarkBase::bench_implementation()
//{
//	return 0;
//}
