#include <fstream>
#include <iostream>
#include <vector>
#include <cstdlib>


using namespace std;

class ReadWriter
{

private:

	fstream fin;
	fstream fin_time;
	fstream fout;

public:

	~ReadWriter()
	{
		fin.close();
		fin_time.close();
		fout.close();
	}

	ReadWriter()
	{
		fin.open("matrix.txt", std::ios::in);
		fin_time.open("times.txt", std::ios::in);
	}



	//чтение матрицы размера size*size из файла
	void readMatrix(vector<vector<int>>& arr, int size)
	{
		for (int i = 0; i < size; i++) {
			vector<int> v(size, 0);
			arr.push_back(v);
			for (int j = 0; j < size; j++) {
				fin >> arr[i][j];
			}
		}
	}



};




class Column
{
public:
	vector<int> arr;

	Column(int size)
	{
		//нужно создать столько бит, чтобы хватило для заполнения size 0 и 1
		//каждый int по 32 бита
		int amount = size / 32 + 1;
		for (int am = 0; am < amount; am++) {
			arr.push_back(0);
		}
	}


	Column(int size, int i, int j)
	{
		//нужно создать столько бит, чтобы хватило для заполнения size 0 и 1
		//каждый int по 32 бита
		int amount = size / 32 + 1;
		for (int am = 0; am < amount; am++) {
			arr.push_back(0);
		}
		//arr[0] - самый старший массив бит
		int num = i / 32; //номер числа int в векторе
		int num2 = i % 32; //номер бита в числе
		int mask = 1 << num2;
		arr[num] |= mask;

		num = j / 32;
		num2 = j % 32;
		mask = 1 << num2;
		arr[num] |= mask;
	}

	Column(int size, int i, int j, int k)
	{
		//нужно создать столько бит, чтобы хватило для заполнения size 0 и 1
		//каждый int по 32 бита
		int amount = size / 32 + 1;
		for (int am = 0; am < amount; am++) {
			arr.push_back(0);
		}
		//arr[0] - самый старший массив бит
		int num = i / 32; //номер числа int в векторе
		int num2 = i % 32; //номер бита в числе
		int mask = 1 << num2;
		arr[num] |= mask;

		num = j / 32;
		num2 = j % 32;
		mask = 1 << num2;
		arr[num] |= mask;

		num = k / 32;
		num2 = k % 32;
		mask = 1 << num2;
		arr[num] |= mask;
	}

	Column(int size, int i, int j, int k, int l)
	{
		//нужно создать столько бит, чтобы хватило для заполнения size 0 и 1
		//каждый int по 32 бита
		int amount = size / 32 + 1;
		for (int am = 0; am < amount; am++) {
			arr.push_back(0);
		}
		//arr[0] - самый старший массив бит
		int num = i / 32; //номер числа int в векторе
		int num2 = i % 32; //номер бита в числе
		int mask = 1 << num2;
		arr[num] |= mask;

		num = j / 32;
		num2 = j % 32;
		mask = 1 << num2;
		arr[num] |= mask;

		num = k / 32;
		num2 = k % 32;
		mask = 1 << num2;
		arr[num] |= mask;

		num = l / 32;
		num2 = l % 32;
		mask = 1 << num2;
		arr[num] |= mask;
	}

	int getLow() {
		//найти старший бит, которые не равен нулю
		for (int i = arr.size() - 1; i >= 0; i--) {
			if (arr[i] != 0) {
				//надо найти старший ненулевой бит этого числа
				for (int j = 31; j >= 0; j--) {
					int mask = 1 << j;
					if ((mask | arr[i]) == arr[i]) {
						return i*32 + j;
					}
				}
			}
		}
		return -1;
	}

	void Add(Column& j) {
		for (int i = 0; i < arr.size(); i++) {
			this->arr[i] = this->arr[i] ^ j.arr[i];
		}
	}
};


vector<vector<int>> m_space;
vector<int> times; //время появления каждого симплекса. Размер вектора = кол-во симплексов
vector<pair<int, int>> intervals;
ReadWriter rw;

int vertexes;
int lines;
int triangles;
int tetrahedrons;

void amount_of_lines() {
	lines = 0;
	for (int i = 0; i < m_space.size(); i++) {
		for (int j = i + 1; j < m_space.size(); j++) {
			if (m_space[i][j] != INT32_MAX) {
				lines++; //нашли отрезок
			}
		}
	}
}
void amount_of_triangles() {
	int n = m_space.size();
	triangles = 0;
	for (int i = 0; i < n; i++) {
		for (int j = i + 1; j < n; j++) {
			if (m_space[i][j] != INT32_MAX) { //нашлось ребро
				for (int k = max(i, j) + 1; k < n; k++) {
					if (m_space[j][k] != INT32_MAX && m_space[i][k] != INT32_MAX) {
						triangles++;
					}
				}
			}
		}
	}

}
void amount_of_tetrahedrons() {
	int n = m_space.size();
	tetrahedrons = 0;
	for (int i = 0; i < n; i++) {
		for (int j = i + 1; j < n; j++) {
			if (m_space[i][j] != INT32_MAX) { //нашлось ребро
				for (int k = j + 1; k < n; k++) {
					if (m_space[j][k] != INT32_MAX && m_space[i][k] != INT32_MAX) { //нашелся треугольник
						for (int l = k + 1; l < n; l++) {
							if (m_space[l][k] != INT32_MAX && m_space[l][i] != INT32_MAX && m_space[l][j] != INT32_MAX) {
								tetrahedrons++;
							}
						}
					}
				}
			}
		}
	}
}

//из точек в отрезки
void create_dim1(vector<Column>& matrix) {
	cout << "dim1 matrix\n";

	//проходимся по всех отрезкам и заполняем матрицу
	for (int i = 0; i < m_space.size(); i++) {
		for (int j = i + 1; j < m_space.size(); j++) {
			if (m_space[i][j] != INT32_MAX) {
				matrix.push_back(Column(vertexes + lines + triangles + tetrahedrons, i, j));
				//matrix[lines] - i, j бит единицы, все остальные нули 
				//время появления комплекса
				times.push_back(m_space[i][j]); //время = длина отрезка
			}
		}
	}

}


//из отрезков в треугольники
void create_dim2(vector<Column>& matrix) {
	cout << "dim2 matrix\n";

	//проходимся по всем треугольникам и заполняем матрицу
	for (int i = 0; i < vertexes; i++) {
		for (int j = i + 1; j < vertexes; j++) {
			if (m_space[i][j] != INT32_MAX) { //нашлось ребро
				for (int k = max(i, j) + 1; k < vertexes; k++) {
					if (m_space[j][k] != INT32_MAX && m_space[i][k] != INT32_MAX) {
						matrix.push_back(Column(vertexes + lines + triangles + tetrahedrons, 
							vertexes + i, vertexes + j, vertexes + k));
						//время появления комплекса
						times.push_back(max(max(m_space[i][j], m_space[i][k]), m_space[j][k])); //время = max(грани треугольника)

					}
				}
			}
		}
	}
}


//из треугольников в тетраэдры
void create_dim3(vector<Column>& matrix) {
	cout << "dim3 matrix\n";

	//проходимся по всем тетраэдрам и заполняем матрицу
	int tetrahedron = 0;
	for (int i = 0; i < vertexes; i++) {
		for (int j = i + 1; j < vertexes; j++) {
			if (m_space[i][j] != INT32_MAX) { //нашлось ребро
				for (int k = j + 1; k < vertexes; k++) {
					if (m_space[j][k] != INT32_MAX && m_space[i][k] != INT32_MAX) { //нашелся треугольник
						for (int l = k + 1; l < vertexes; l++) {
							if (m_space[l][k] != INT32_MAX && m_space[l][i] != INT32_MAX && m_space[l][j] != INT32_MAX) { //нашелся тетраэдр
								matrix.push_back(Column(vertexes + lines + triangles + tetrahedrons,
									vertexes + lines + i, vertexes + lines + j, vertexes + lines + k, vertexes + lines + l));

								//время появления комплекса
								times.push_back(
									max(
										max(
											max(m_space[i][j], m_space[i][k]),
											max(m_space[i][l], m_space[j][k])
										),
										max(m_space[j][l], m_space[k][l])
									)
								); //время = max(ребра тетаэдра)

								tetrahedron++;
							}
						}
					}
				}
			}
		}
	}
}



//всего есть 
//вершин: size
//отрезков: lines
//треугольников: triangles
//тетраэдров: tetrahedrons



//low(num = номер столбца)
//-1, если столбец состоит из нулей
int low(vector<Column>& matrix, int num) {
	return matrix[num].getLow();
}

//add column i to column j
void add(vector<Column>& matrix, int i, int j) {
		matrix[j].Add(matrix[i]); //F2
}


//алгоритм приведения матрицы к нужной форме
void reducing(vector<Column>& matrix) {
	int n = vertexes + lines + triangles + tetrahedrons;
	for (int j = 0; j < n; j++) {
		bool exit = false;
		while (!exit) { //если за один проход не нашелся ни один подходящий столбец, то выходим
			exit = true;
			for (int i = 0; i < j; i++) {
				int low_i = low(matrix, i);
				int low_j = low(matrix, j);
				if (low_i != -1 && low_i == low_j) {
					exit = false; //если есть хотя бы один подходящий столбец
					add(matrix, i, j);
					//пошаговый вывод матрицы для проверки
					/*cout << "\n\n";
					for (int i = 0; i < matrix.size(); i++) {
						for (int j = 0; j < matrix[i].size(); j++) {
							cout << matrix[i][j] << ' ';
						}
						cout << '\n';
					}*/
				}
			}
		}
	}
}


void change_lows(vector<int>& lows, int size) {

	for (int i = size + lines; i < size + lines + triangles; i++) {
		lows[i] -= 1;
	}
	for (int i = size + lines + triangles; i < size + lines + triangles + tetrahedrons; i++) {
		lows[i] -= 2;
	}
}


void reading_intervals(vector<Column>& matrix) {
	int n = vertexes + lines + triangles + tetrahedrons;
	vector<int> lows(n); //вектор всех нижних граней
	for (int i = 0; i < n; i++)
		lows[i] = low(matrix, i);
	change_lows(lows, vertexes);

	for (int i = 0; i < n; i++) {
		if (lows[i] == -1) { //undefined
			//найти такое k, что low(k) == i. Тогда k - конец, i - старт
			bool is_paired = false;
			for (int k = i; k < n; k++) {
				if (lows[k] == i) {
					intervals.push_back({ times[i], times[k] });
					cout << times[i] << ' ' << times[k] << "   " << i << ' ' << k << '\n';
					is_paired = true;
					break;
				}
			}
			if (!is_paired) {
				intervals.push_back({ times[i], INT32_MAX });  // [i; +inf) - интервал
				cout << times[i] << ' ' << INT32_MAX << "   " << i << '\n';
			}
		}
		else {
			//надо найти такое j, что j = low(i). Тогда j - старт, i - конец
			///for (int j = 0; j < n; j++) {
			for (int j = i; j < n; j++) {
				if (j == lows[i]) {
					intervals.push_back({ times[j], times[i] });
					cout << times[j] << ' ' << times[i] << "   " << j << ' ' << i << '\n';
					break;
				}
			}
		}
	}
}




void process() {
	vertexes = 3738;

	cout << "Reading matrix \n";
	rw.readMatrix(m_space, vertexes);

	
	vector<Column> matrix;

	for (int i = 0; i < vertexes; i++) {
		times.push_back(0);
	}

	amount_of_lines();
	amount_of_triangles();
	//amount_of_tetrahedrons();
	cout << "lines = " << lines << '\n';
	cout << "tr = " << triangles << '\n';
	//cout << "tetr = " << tetrahedrons << '\n';
	tetrahedrons = 0;
	for(int i = 0; i < vertexes; i++)
		matrix.push_back(Column(vertexes + lines + triangles + tetrahedrons));

	create_dim1(matrix);
	create_dim2(matrix);
	//create_dim3(matrix);


	reducing(matrix);


	reading_intervals(matrix);
	cout << "intervals:\n\n\n";
	/*for (int i = 0; i < intervals.size(); i++) {
		cout << intervals[i].first << ' ' << intervals[i].second << '\n';
	}*/
}



int main()
{
	process();
}
