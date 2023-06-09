pipeline {
    agent any
    
    stages {
        stage('Checkout') {
          steps {
            // Checkout the repository
            checkout([$class: 'GitSCM', branches: [[name: '*/main']], userRemoteConfigs: [[url: 'https://github.com/Mariott-Dev/TestBackendAPI']]])
          }
        }
        
        stage('Restore Packages') {
            steps {
                bat 'dotnet restore'
            }
        }
        
        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }
        
        stage('Run Tests') {
            steps {
                bat 'dotnet test'
            }
        }
        
        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o ./publish'
            }
        }
    }
}

