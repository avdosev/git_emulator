using System;
using System.Collections.Generic;
using System.Linq;

namespace GitTask
{
    internal class GitCommit {
        public int FileContent = -1, CommitNumber = -1;

        public GitCommit(int fileContent, int commitNumber) {
            FileContent = fileContent;
            CommitNumber = commitNumber;
        }
    }

    public class Git {
        private List<List<GitCommit>> commitsFiles;
        private List<int> currentFiles;
        private int countCallsCommit = -1;

        public Git(int filesCount) {
            commitsFiles = new List<List<GitCommit>>(filesCount);
            currentFiles = new List<int>(filesCount);
            for (var filename = 0; filename < filesCount; filename++) {
                commitsFiles.Add(new List<GitCommit>());
                commitsFiles[filename].Add(new GitCommit(-1, -1));
                currentFiles.Add(-1);
            }
        }
        ///  меняет содержимое файла fileNumber на value
        public void Update(int fileNumber, int value) {
            currentFiles[fileNumber] = value;
        }
        
        /// фиксирует текущее состояние файлов, возвращает commitNumber: число раз, которое был вызван Commit() минус 1
        public int Commit() {
            countCallsCommit += 1;

            for (var filename = 0; filename < currentFiles.Count; filename++) {
                if (currentFiles[filename] != commitsFiles[filename].Last().FileContent) {
                    commitsFiles[filename].Add(new GitCommit(currentFiles[filename], countCallsCommit));
                }
            }
            
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

            fileContent = commitsFiles[fileNumber][indexOfNearestCommit].FileContent;
            
            return fileContent;
        }
    }
    
}