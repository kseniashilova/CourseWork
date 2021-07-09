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
		fout.open("vertexes.txt", std::ios::out);
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


	void writeResultVert(const vector<int>& v)
	{
		for (int i = 0; i < v.size(); i++)
			fout << v[i] << '\n';
	}

};


vector<vector<int>> m_space;
vector<int> times; //время появления каждого симплекса. Размер вектора = кол-во симплексов
vector<pair<int, int>> intervals;
ReadWriter rw;
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
void create_dim1(vector<vector<short>>& matrix, int size) {
	amount_of_lines();
	for (int i = 0; i < size; i++) {
		vector<short> v(lines, 0);
		matrix.push_back(v);
	}

	//проходимся по всех отрезкам и заполняем матрицу
	int line = 0;
	for (int i = 0; i < m_space.size(); i++) {
		for (int j = i + 1; j < m_space.size(); j++) {
			if (m_space[i][j] != INT32_MAX) {
				matrix[i][line] = 1;
				matrix[j][line] = 1;

				//время появления комплекса
				times.push_back(m_space[i][j]); //время = длина отрезка

				line++;
			}
		}
	}

}


//из отрезков в треугольники
void create_dim2(vector<vector<short>>& matrix, int size) {
	amount_of_triangles();
	cout << triangles << '\n';

	for (int i = 0; i < size; i++) {
		vector<short> v(triangles, 0);
		matrix.push_back(v);
	}

	//проходимся по всем треугольникам и заполняем матрицу
	int triangle = 0;
	for (int i = 0; i < size; i++) {
		for (int j = i + 1; j < size; j++) {
			if (m_space[i][j] != INT32_MAX) { //нашлось ребро
				for (int k = max(i, j) + 1; k < size; k++) {
					if (m_space[j][k] != INT32_MAX && m_space[i][k] != INT32_MAX) {
						matrix[i][triangle] = 1;
						matrix[j][triangle] = 1;
						matrix[k][triangle] = 1;

						//время появления комплекса
						times.push_back(max(max(m_space[i][j], m_space[i][k]), m_space[j][k])); //время = max(грани треугольника)

						triangle++;
					}
				}
			}
		}
	}
}


//из треугольников в тетраэдры
void create_dim3(vector<vector<short>>& matrix, int size) {
	amount_of_tetrahedrons();
	cout << tetrahedrons << '\n';

	for (int i = 0; i < size; i++) {
		vector<short> v(tetrahedrons, 0);
		matrix.push_back(v);
	}

	//проходимся по всем тетраэдрам и заполняем матрицу
	int tetrahedron = 0;
	for (int i = 0; i < size; i++) {
		for (int j = i + 1; j < size; j++) {
			if (m_space[i][j] != INT32_MAX) { //нашлось ребро
				for (int k = j + 1; k < size; k++) {
					if (m_space[j][k] != INT32_MAX && m_space[i][k] != INT32_MAX) { //нашелся треугольник
						for (int l = k + 1; l < size; l++) {
							if (m_space[l][k] != INT32_MAX && m_space[l][i] != INT32_MAX && m_space[l][j] != INT32_MAX) { //нашелся тетраэдр
								matrix[i][tetrahedron] = 1;
								matrix[j][tetrahedron] = 1;
								matrix[k][tetrahedron] = 1;
								matrix[l][tetrahedron] = 1;

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


//low(num = номер столбца)
//-1, если столбец состоит из нулей
int low(vector<vector<short>>& matrix, int num) {
	for (int i = matrix.size() - 1; i >= 0; i--) {
		if (matrix[i][num] != 0) {
			return i;
		}
	}
	return -1;
}

//add column i to column j
void add(vector<vector<short>>& matrix, int i, int j) {
	for (int k = 0; k < matrix.size(); k++) {
		matrix[k][j] = (matrix[k][j] + matrix[k][i]) % 2; //F2
	}
}


//алгоритм приведения матрицы к нужной форме
void reducing(vector<vector<short>>& matrix) {
	int n = matrix[0].size();
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


void reading_intervals(vector<vector<short>>& matrix) {
	int n = matrix[0].size();
	vector<int> lows(n); //вектор всех нижних граней
	for (int i = 0; i < n; i++)
		lows[i] = low(matrix, i);

	for (int i = 0; i < n; i++) {
		if (lows[i] == -1) { //undefined
			//найти такое k, что low(k) == i. Тогда k - конец, i - старт
			bool is_paired = false;
			for (int k = 0; k < n; k++) {
				if (lows[k] - 1 == i) {
					intervals.push_back({ times[i], times[k] });
					is_paired = true;
					break;
				}
			}
			if (!is_paired) {
				intervals.push_back({ times[i], INT32_MAX });  // [i; +inf) - интервал
			}
		}
		else {
			//надо найти такое j, что j = low(i). Тогда j - старт, i - конец
			for (int j = 0; j < n; j++) {
				if (j == lows[i] - 1) {
					intervals.push_back({ times[j], times[i] });
					break;
				}
			}
		}
	}
}

void process() {
	int size = 3;

	cout << "Reading matrix \n";
	rw.readMatrix(m_space, size);

	int dim;
	cout << "Enter dimension: \n";
	cin >> dim;
	vector<vector<short>> matrix;

	//надо составить матрицу с кол-вом строк, равным количеству вершин
	//и кол-вом столбцов, равным кол-ву отрезков в графе
	//если dim = 1
	if (dim == 1) {
		create_dim1(matrix, size);
	}
	else if (dim == 2) {
		create_dim2(matrix, size);
	}
	else if (dim == 3) {
		create_dim3(matrix, size);
	}


	cout << "matrix:\n\n\n";
	for (int i = 0; i < matrix.size(); i++) {
		for (int j = 0; j < matrix[i].size(); j++) {
			cout << matrix[i][j] << ' ';
		}
		cout << '\n';
	}

	reducing(matrix);

	cout << "reduced matrix:\n\n\n";
	for (int i = 0; i < matrix.size(); i++) {
		for (int j = 0; j < matrix[i].size(); j++) {
			cout << matrix[i][j] << ' ';
		}
		cout << '\n';
	}

	reading_intervals(matrix);
	cout << "intervals:\n\n\n";
	for (int i = 0; i < intervals.size(); i++) {
		cout << intervals[i].first << ' ' << intervals[i].second << '\n';
	}
}



int main()
{
	process();
}
