pipeline {
  agent any

  stages {
    stage('Checkout') {
      steps {
        git url: 'https://github.com/sabbirmusfique/Task-4-Game.git', branch: 'main'
      }
    }

    stage('Build') {
      steps {
        bat 'git --version'
        bat 'dir'
      }
    }

  }

  post {
    success {
      echo 'Test done'
    }
  }
}
