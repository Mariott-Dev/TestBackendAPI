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
                script {
                    bat 'dotnet restore TestBackendAPI.sln'
                }
            }
        }
        
        stage('Build') {
            steps {
                script {
                    bat 'dotnet build TestBackendAPI.sln'
                }
            }
        }
        
        stage('Run Tests') {
            steps {
                sctript {
                    bat 'dotnet test TestBackendAPI.sln'
                }
            }
        }
        
        stage('Publish') {
            steps {
                script {
                    bat 'dotnet publish TestBackendAPI.sln -c Release -o ./publish'
                }
            }
        }
    }
}

