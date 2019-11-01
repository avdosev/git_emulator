using System;
using System.Collections.Generic;
using System.Linq;

namespace GitTask
{
    internal class GitCommit {
        public int FileContent, CommitNumber;

        public GitCommit(int fileContent, int commitNumber) {
            FileContent = fileContent;
            CommitNumber = commitNumber;
        }
    }

    public class Git {
        private List<List<GitCommit>> commitsFiles;
        private List<int> currentFiles;
        private SortedSet<int> indexsChangedFiles;
        private int countCallsCommit = -1;

        public Git(int filesCount) {
            commitsFiles = new List<List<GitCommit>>(filesCount);
            currentFiles = new List<int>(filesCount);
            indexsChangedFiles = new SortedSet<int>();
            for (var filename = 0; filename < filesCount; filename++) {
                commitsFiles.Add(new List<GitCommit>());
                commitsFiles[filename].Add(new GitCommit(0, -1));
                currentFiles.Add(0);
            }
        }
        ///  меняет содержимое файла fileNumber на value
        public void Update(int fileNumber, int value) {
            currentFiles[fileNumber] = value;
            indexsChangedFiles.Add(fileNumber);
        }
        
        /// фиксирует текущее состояние файлов, возвращает commitNumber: число раз, которое был вызван Commit() минус 1
        public int Commit() {
            countCallsCommit += 1;

            foreach (var filename in indexsChangedFiles) {
                if (currentFiles[filename] != commitsFiles[filename].Last().FileContent) {
                    commitsFiles[filename].Add(new GitCommit(currentFiles[filename], countCallsCommit));
                }
            }
            
            indexsChangedFiles.Clear();
            
            return countCallsCommit;
        }

        /// возвращает содержимое файла fileNumber на момент коммита commitNumber. Если этого коммита ещё не было — бросает ArgumentException
        public int Checkout(int commitNumber, int fileNumber) {
            int fileContent = -1;
            bool commitFound = commitNumber <= countCallsCommit;

            if (!commitFound)
                throw new ArgumentException();

            var indexOfNearestCommit = commitsFiles[fileNumber].BinarySearch(new GitCommit(-1, commitNumber),
                Comparer<GitCommit>.Create( 
                    (x, y) => x.CommitNumber.CompareTo(y.CommitNumber) 
                    )
                );
            
            // бинарный поиск в мире сишарпа - самое дрыстнявое изобретение даунов
            // где мои стл итераторы(((
            if (indexOfNearestCommit < 0) {
                indexOfNearestCommit = ~indexOfNearestCommit - 1;
                if (indexOfNearestCommit < 0) {
                    return 0;
                }
                

            } 
            
            fileContent = commitsFiles[fileNumber][indexOfNearestCommit].FileContent;

            return fileContent;
        }
    }
    
}