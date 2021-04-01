#include "CrystalBenchmarkBase.h"
#include <iostream>

int CrystalBenchmarkBase::bench(int threads) {
	std::atomic_bool cancellation_token;
	//std::function<int(std::atomic_bool&)> callback = std::bind(&CrystalBenchmarkBase::benchImplementation, this, std::ref(cancellation_token));

	int result = this->benchAll(threads);
	return result;
}

int CrystalBenchmarkBase::benchAll(int threads)
{
	std::atomic_bool cancellation_token;
	auto f = std::async(&CrystalBenchmarkBase::benchImplementation, this, std::ref(cancellation_token));

	std::chrono::milliseconds span(10000);

	while (f.wait_for(span) == std::future_status::timeout)	  {
		cout << "e" << "\n";
		cancellation_token = true;
	}
		
	int x = f.get();

	return x;
}

//int CrystalBenchmarkBase::benchImplementation()
//{
//	return 0;
//}
