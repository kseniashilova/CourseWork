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

	for (int i = 0; i < lines; i++) {
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

	for (int i = 0; i < triangles; i++) {
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



//всего есть 
//вершин: size
//отрезков: lines
//треугольников: triangles
//тетраэдров: tetrahedrons

void combine_matrixes(vector<vector<short>>& matrix1, vector<vector<short>>& matrix2,
	vector<vector<short>>& matrix3, vector<vector<short>>& matrix, int size) {
	int n = size + lines + triangles + tetrahedrons; //размер общей матрицы

	for (int i = 0; i < n; i++) {
		vector<short> vec(n, 0);
		matrix.push_back(vec);
	}


	for (int i = size; i < size + lines; i++) {//lines - столбцов
		for (int j = 0; j < size; j++) { //size - строк
			matrix[j][i] = matrix1[j][i - size];
		}
	}


	for (int i = size + lines;
		i < size + lines + triangles; i++) { //triangles столбцов
		for (int j = size; j < size + lines; j++) {//lines строк
			matrix[j][i] = matrix2[j - size][i - size - lines];
		}
	}


	for (int i = size + lines + triangles;
		i < size + lines + triangles + tetrahedrons; i++) { //tetrahedrons столбцов
		for (int j = size + lines; j < size + lines + triangles; j++) { // triangles строк
			matrix[j][i] = matrix3[j - size - lines][i - size - lines - triangles];
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


void change_lows(vector<int>& lows, int size) {
	
	for (int i = size+lines; i < size + lines + triangles; i++) {
		lows[i]-=1;
	}
	for (int i = size + lines + triangles; i < size + lines + triangles + tetrahedrons; i++) {
		lows[i] -= 2;
	}
}


void reading_intervals(vector<vector<short>>& matrix, int size) {
	int n = matrix[0].size();
	vector<int> lows(n); //вектор всех нижних граней
	for (int i = 0; i < n; i++)
		lows[i] = low(matrix, i);
	change_lows(lows, size);

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
	int size = 3738;

	cout << "Reading matrix \n";
	rw.readMatrix(m_space, size);

	vector<vector<short>> matrix1;
	vector<vector<short>> matrix2;
	vector<vector<short>> matrix3;
	vector<vector<short>> matrix;

	for (int i = 0; i < size; i++) {
		times.push_back(0);
	}

	create_dim1(matrix1, size);
	create_dim2(matrix2, size);
	create_dim3(matrix3, size);

	combine_matrixes(matrix1, matrix2, matrix3, matrix, size);


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

	reading_intervals(matrix, size);
	cout << "intervals:\n\n\n";
	/*for (int i = 0; i < intervals.size(); i++) {
		cout << intervals[i].first << ' ' << intervals[i].second << '\n';
	}*/
}



int main()
{
	process();
}
